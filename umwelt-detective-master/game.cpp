/**
 * @file game.cpp
 * @author Niklas Schmidt, Jenny Dietrich, Felix Heilmann
 * @date 09.06.2023
 * @brief Main game file.
 */

#include "game.h"
#include "speech.h"

// INCLUDE MODULES HERE:
#include "water_consumption_module.cpp"
#include "fuel_consumption_module.cpp"

#include <iostream>
#include <alcommon/albroker.h>

#include <sys/socket.h>
#include <sys/types.h>

using namespace std;
using namespace AL;

THWildUmweltDetektiveModule::THWildUmweltDetektiveModule(boost::shared_ptr<ALBroker> broker,
                                                         const std::string &name) : ALModule(broker, name)
{
    // bind all methods for use
    setModuleDescription("The module for the envireoment detective game.");

    functionName("startGame", getName(), "Starts the game");
    BIND_METHOD(THWildUmweltDetektiveModule::startGame);

    functionName("explainGame", getName(), "Explains the game");
    BIND_METHOD(THWildUmweltDetektiveModule::explainGame);

    functionName("endGame", getName(), "Ends the game.");
    BIND_METHOD(THWildUmweltDetektiveModule::endGame);

    functionName("selectModule", getName(), "To select a topic to play.");
    BIND_METHOD(THWildUmweltDetektiveModule::selectModule);

    functionName("gameLogic", getName(), "Main Game Logic.");
    BIND_METHOD(THWildUmweltDetektiveModule::gameLogic);

    functionName("getQRCode", getName(), "To get QRCode.");
    BIND_METHOD(THWildUmweltDetektiveModule::getQRCode);

    functionName("nextModule", getName(), "To select next module.");
    BIND_METHOD(THWildUmweltDetektiveModule::nextModule);

    functionName("previousModule", getName(), "To select previous module.");
    BIND_METHOD(THWildUmweltDetektiveModule::previousModule);
}

/**
 * Destructor for THWildUmweltDetektiveModule
 * Unsubscribes from all events
 */
THWildUmweltDetektiveModule::~THWildUmweltDetektiveModule()
{
    vector<string> vecEvents;
    vecEvents.push_back("FrontTactilTouched");
    vecEvents.push_back("RearTactilTouched");
    vecEvents.push_back("MiddleTactilTouched");
    vecEvents.push_back("LeftBumperPressed");
    vecEvents.push_back("RightBumperPressed");
    vecEvents.push_back("THWildQRCodeReader/BarcodeData");
    vector<string> vecSubscribers;
    for (int i = 0; i < vecEvents.size(); i++)
    {
        vecSubscribers = almp.getSubscribers(vecEvents[i]);
        for (int j = 0; j < vecSubscribers.size(); j++)
        {
            if (vecSubscribers[j] == "THWildUmweltDetektiveModule")
            {
                almp.unsubscribeToEvent(vecEvents[i], "THWildUmweltDetektiveModule");
            }
        }
    }
}

/**
 * Initializes all neccessary variables for the gameplay
 */
void THWildUmweltDetektiveModule::init()
{
    std::cout << "init" << std::endl;

    almp = ALMemoryProxy(getParentBroker());

    // create a SpeechHelper to manage all speeches
    speech = new SpeechHelper();

    // ADD MODULES TO LOAD HERE:
    gameModules.push_back(new WaterConsumptionModule());
    gameModules.push_back(new FuelConsumptionModule());
}

/**
 * Method, which is called to start the game.
 * The robot introduces itself.
 */
void THWildUmweltDetektiveModule::startGame()
{
    try
    {
        // prevent, that the game gets started again
        almp.unsubscribeToEvent("RightBumperPressed", "THWildUmweltDetektiveModule");
        speech->cancelSpeech();
        qi::os::msleep(500);

        // the button on the back of the head leads to a game explanation, front starts game
        almp.subscribeToEvent("FrontTactilTouched", "THWildUmweltDetektiveModule", "selectModule");
        almp.subscribeToEvent("RearTactilTouched", "THWildUmweltDetektiveModule", "explainGame");

        // inform the user about the choice
        speech->sayWithoutChoice(1);
    }
    catch (const AL::ALError &e)
    {
        qiLogError("THWildUmweltDetektiveModule::startGame: ") << e.what() << std::endl;
    }
}

/**
 * Method, which lets the NAO explain the game rules
 */
void THWildUmweltDetektiveModule::explainGame()
{
    // unsubscribe from events
    almp.unsubscribeToEvent("RearTactilTouched", "THWildUmweltDetektiveModule");
    // cancelling the speech output to allow a fast transition to the game itself
    speech->cancelSpeech();
    try
    {
        // next step: difficulty selection
        qi::os::msleep(500);
        almp.subscribeToEvent("RearTactilTouched", "THWildUmweltDetektiveModule", "endGame");
        // explain the rules
        speech->sayWithoutChoice(2);
    }
    catch (const AL::ALError &e)
    {
        qiLogError("THWildUmweltDetektiveModule::explainGame: ") << e.what() << std::endl;
    }
}

/**
 * Method, to select the Module to be played
 */
void THWildUmweltDetektiveModule::selectModule()
{
    // unsubscribe to both events to allow a correct gameplay
    almp.unsubscribeToEvent("FrontTactilTouched", "THWildUmweltDetektiveModule");
    almp.unsubscribeToEvent("RearTactilTouched", "THWildUmweltDetektiveModule");

    // cancelling the speech output to allow a fast transition to the game itself
    speech->cancelSpeech();
    try
    {
        qi::os::msleep(500);
        // select first game module in vector
        selectedModule = gameModules.front();
        moduleIndex = 0;

        // subscribe to events for module selection
        almp.subscribeToEvent("LeftBumperPressed", "THWildUmweltDetektiveModule", "nextModule");
        almp.subscribeToEvent("RightBumperPressed", "THWildUmweltDetektiveModule", "previousModule");
        almp.subscribeToEvent("FrontTactilTouched", "THWildUmweltDetektiveModule", "gameLogic");
        almp.subscribeToEvent("RearTactilTouched", "THWildUmweltDetektiveModule", "endGame");

        speech->sayWithoutChoice(3);

        // say all selectable modules
        qi::os::msleep(500);
        for (int i = 0; i < gameModules.size(); i++)
        {
            speech->cancelSpeech();
            std::string gameModuleName = gameModules[i]->getName();
            speech->sayStringMessage(gameModuleName);
            qi::os::msleep(500);
        }

        // explain how to select module
        speech->sayWithoutChoice(4);
        qi::os::msleep(500);

        // Standard selection
        std::string message = gameModules.front()->getName().append(" ist derzeit ausgewählt. ");
        speech->sayStringMessage(message);
    }
    catch (const AL::ALError &e)
    {
        qiLogError("THWildUmweltDetektiveModule::selectModule: ") << e.what() << std::endl;
    }
}

/**
 * Method, which is called to select next module.
 */
void THWildUmweltDetektiveModule::nextModule()
{
    // unsubcribe so there is no second unvolountary switch
    almp.unsubscribeToEvent("LeftBumperPressed", "THWildUmweltDetektiveModule");
    almp.unsubscribeToEvent("RightBumperPressed", "THWildUmweltDetektiveModule");
    speech->cancelSpeech();
    try
    {
        qi::os::msleep(500);
        moduleIndex = (moduleIndex + 1) % gameModules.size();

        // ToDo: Er sagt es irgendwie mehrfach wenn du schnell hintereinander gedrückt
        selectedModule = gameModules[moduleIndex];
        std::string message2 = gameModules[moduleIndex]->getName().append(", ist nun ausgewählt. ");
        speech->sayStringMessage(message2);

        almp.subscribeToEvent("LeftBumperPressed", "THWildUmweltDetektiveModule", "nextModule");
        almp.subscribeToEvent("RightBumperPressed", "THWildUmweltDetektiveModule", "previousModule");
    }
    catch (const AL::ALError &e)
    {
        qiLogError("THWildUmweltDetektiveModule::selectModule: ") << e.what() << std::endl;
    }
}

/**
 * Method, which is called to select previous module.
 */
void THWildUmweltDetektiveModule::previousModule()
{
    // unsubcribe so there is no second unvolountary switch
    almp.unsubscribeToEvent("LeftBumperPressed", "THWildUmweltDetektiveModule");
    almp.unsubscribeToEvent("RightBumperPressed", "THWildUmweltDetektiveModule");
    speech->cancelSpeech();
    try
    {
        qi::os::msleep(500);
        moduleIndex = (moduleIndex - 1 + gameModules.size()) % gameModules.size();

        // ToDo: Er sagt es irgendwie mehrfach wenn du schnell hintereinander gedrückt
        selectedModule = gameModules[moduleIndex];
        std::string message3 = gameModules[moduleIndex]->getName().append(", ist nun ausgewählt. ");
        speech->sayStringMessage(message3);

        almp.subscribeToEvent("LeftBumperPressed", "THWildUmweltDetektiveModule", "nextModule");
        almp.subscribeToEvent("RightBumperPressed", "THWildUmweltDetektiveModule", "previousModule");
    }
    catch (const AL::ALError &e)
    {
        qiLogError("THWildUmweltDetektiveModule::selectModule: ") << e.what() << std::endl;
    }
}

/**
 * Method, with the main game logic
 */
void THWildUmweltDetektiveModule::gameLogic()
{
    noQRCode = true;
    gameComplete = false;
    // unsubscribe to both events to allow a correct gameplay
    almp.unsubscribeToEvent("FrontTactilTouched", "THWildUmweltDetektiveModule");
    almp.unsubscribeToEvent("RightBumperPressed", "THWildUmweltDetektiveModule");
    almp.unsubscribeToEvent("LeftBumperPressed", "THWildUmweltDetektiveModule");

    // cancelling the speech output to allow a fast transition to the game itself
    speech->cancelSpeech();
    try
    {
        // say indroduction into module
        std::string modulIntroduction = selectedModule->getModuleIntroduction();
        speech->sayStringMessage(modulIntroduction);

        qi::os::msleep(500);

        // say the question of module
        std::string question = selectedModule->getQuestion();

        // get all objects of module
        std::vector<GameObject *> gameObjects = selectedModule->getObjects();
        nextPosition = 1;
        neededTurns = 0;
        gameComplete = false;

        speech->sayStringMessage(question);

        // main game loop
        while (gameComplete == false)
        {
            // subscribe to event to get qrcode
            almp.subscribeToEvent("THWildQRCodeReader/BarcodeData", "THWildUmweltDetektiveModule", "getQRCode");
            // next step: scan qrcode
            while (noQRCode == true)
            {
                usleep(10);
            }

            qi::os::msleep(500);
            
            // check next object
            for (int i = 0; i < gameObjects.size(); i++)
            {
                GameObject *gameObject = gameObjects[i];
                if (gameObject->getId() == qrData)
                {
                    // unsubscribe to event to get qrcode so there is no second scan
                    almp.unsubscribeToEvent("THWildQRCodeReader/BarcodeData", "THWildUmweltDetektiveModule");
                    
                    // say name of object that was scanned
                    std::string objectName = gameObject->getName().append(" , ausgewählt.");
                    speech->sayStringMessage(objectName);
                    qi::os::msleep(500);

                    // check if qrcode is correct
                    if (gameObject->getWeight() == nextPosition)
                    {  
                        // say that the decision was correct
                        speech->sayWithChoice(SPEECH_GOODDESICION);
                        speech->sayStringMessage("Hier sind noch einige Infos: ");
                        qi::os::msleep(500);
                        std::string explination = gameObject->getDescription();
                        speech->sayStringMessage(explination);
                        qi::os::msleep(500);

                        if (nextPosition < 5)
                        {
                            // say to select next card
                            speech->sayStringMessage("Wähle nun die nächste Karte. ");
                            nextPosition = nextPosition + 1;
                            neededTurns = neededTurns + 1;
                        }
                        else
                        {
                            // say that the game is complete
                            speech->sayWithChoice(SPEECH_WIN);
                            neededTurns = neededTurns + 1;
                            std::ostringstream finalMessage;
                            qi::os::msleep(500);
                            finalMessage << "Du hast " << neededTurns << " Züge gebraucht.";
                            speech->sayStringMessage(finalMessage.str());
                            gameComplete = true;
                        }
                    }
                    else
                    {
                        // say that the decision was wrong
                        speech->sayWithChoice(SPEECH_WRONGDESICION);
                        neededTurns = neededTurns + 1;
                    }
                    break;
                }
            }

            noQRCode = true;
        }
        qi::os::msleep(500);

        // say sustainabilitytips
        speech->sayStringMessage("Jetzt kommen noch ein paar Tipps zu diesem Thema: ");
        qi::os::msleep(500);
        std::string susTips = selectedModule->getSustainabilityTips();
        speech->sayStringMessage(susTips);

        qi::os::msleep(500);

        // say right foot to play again
        speech->sayStringMessage("Berühre meinen rechten Fuß, um erneut spielen zu können. ");

        THWildUmweltDetektiveModule::endGame();
    }
    catch (const AL::ALError &e)
    {
        qiLogError("THWildUmweltDetektiveModule::selectModule: ") << e.what() << std::endl;
    }
}

/**
 * Method, which lets the NAO get the QR Code
 */
void THWildUmweltDetektiveModule::getQRCode()
{
    // cancelling the speech output to allow a fast transition to the game itself
    speech->cancelSpeech();
    try
    {
        // get the QR Code from the event
        AL::ALValue data = almp.getData("THWildQRCodeReader/BarcodeData");
        qrData = data.toString();

        noQRCode = false;
        qiLogInfo("QR-Code: ") << qrData.data() << std::endl;
    }
    catch (const AL::ALError &e)
    {
        qiLogError("THWildUmweltDetektiveModule::selectModule: ") << e.what() << std::endl;
    }
}

/**
 * Method, that ends the game by saying good bye to the user and unsubscribe to all events
 */
void THWildUmweltDetektiveModule::endGame()
{
    try
    {
        almp.unsubscribeToEvent("RearTactilTouched", "THWildUmweltDetektiveModule");
        speech->cancelSpeech();

        // say goodbye
        speech->sayWithChoice(SPEECH_GOODBYE);
        qrData = "";
        noQRCode = false;
        gameComplete = true;
        // unsubscribe to all events
        vector<string> vecEvents;
        vecEvents.push_back("FrontTactilTouched");
        vecEvents.push_back("RearTactilTouched");
        vecEvents.push_back("MiddleTactilTouched");
        vecEvents.push_back("LeftBumperPressed");
        vector<string> vecSubscribers;
        for (int i = 0; i < vecEvents.size(); i++)
        {
            vecSubscribers = almp.getSubscribers(vecEvents[i]);
            for (int j = 0; j < vecSubscribers.size(); j++)
            {
                if (vecSubscribers[j] == "THWildUmweltDetektiveModule")
                {
                    almp.unsubscribeToEvent(vecEvents[i], "THWildUmweltDetektiveModule");
                }
            }
        }
        almp.subscribeToEvent("RightBumperPressed", "THWildUmweltDetektiveModule", "startGame");
        return;
    }
    catch (const AL::ALError &e)
    {
        qiLogError("THWildUmweltDetektiveModule::endGame: ") << e.what() << std::endl;
    }
}
