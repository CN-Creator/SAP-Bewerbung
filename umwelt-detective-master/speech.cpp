/**
  * Speech-module
  * @author Niklas Schmidt, Jenny Dietrich, Felix Heilmann
  * Inspired by "Connect4" Project
  **/

#include "speech.h"

using namespace std;

/**
 * constructor for the speech helper
 * adding all texts to the speech vectors
 */
SpeechHelper::SpeechHelper()
{

    beginMessage = "Hallo, ich bin Lino. Ich bin heute dein Spielpartner für eine Runde Umwelt Detektiv. "
                   "Wenn du direkt loslegen möchtest, drücke bitte die vordere Taste auf meinem Kopf. "
                   "Falls ich dir die Spielregeln erklären soll, drücke bitte die hintere Taste auf meinem Kopf.";
    explanationMessage = "Die Spielregeln sind nicht kompliziert. Alles ganz easy. "
                         "Wähle als erstes ein Thema, welches du spielen möchtest. "
                         "Ich werde dir dann eine Frage stellen, mit der sich die Objekte ordnen lassen. "
                         "Beginne mit dem Objekt, welches den geringsten Umwelteinfluss hat und arbeite dich der Reihe nach hoch. "
                         "Stecke dafür eine Karte so in die Forschungsstation, dass das Bild zu dir zeigt und der QR-Code zu mir. "
                         "Dann kann ich mit der Analyse beginnen. "
                         "Ich werde dir nach jedem Objekt Feedback zu deiner Entscheidung geben. "
                         "Bist du bereit, loszulegen? Dann tippe mir vorne auf den Kopf.";
    selectModuleMessage = "Du kannst dich zwischen verschiedenen Themen entscheiden. "
                          "Wähle das Thema, was dich am meisten interessiert. "
                          "Es gibt folgende Themen. ";
    selectModuleExplanationMessage = "Nun da du alle Themen gehört hast, kannst du zwischen diesen Wechseln. "
                                     "Drücke meinen linken Fuß, um zum nächsten Thema zu wechseln und meinen rechten Fuß um zurück zu wechseln. "
                                     "Drücke meinen Kopf um die Auswahl zu bestätigen und das Spiel zu starten. ";

    vecWin.push_back("Super, Du hast gewonnen!");
    vecWin.push_back("Gewonnen: Du warst siegreich!");
    vecWin.push_back("Gewonnen: Das hast du super gemacht.");
    vecWin.push_back("Gewonnen: Du bist unschlagbar!");
    vecWin.push_back("Gewonnen: Das nächste mal wird es schwieriger.");
    vecWin.push_back("Gewonnen: Du bist klasse!");
    vecWin.push_back("Gewonnen: Du bist zwar nicht der Terminator, aber du hast dich trotzdem super geschlagen!");
    vecWin.push_back("Gewonnen: Du rockst!");
    vecWin.push_back("Gewonnen: Du bist ein \\rspd=85\\Gewinner!\\rst\\");
    vecWin.push_back("Gewonnen: Juuhuu!");

    vecGoodBye.push_back("Auf Wiedersehen.");
    vecGoodBye.push_back("Bis zum naechsten Mal.");
    vecGoodBye.push_back("Tschuessi.");
    vecGoodBye.push_back("Bis dann.");
    vecGoodBye.push_back("Bis bald.");
    vecGoodBye.push_back("\\rspd=90\\Tschoe mit oe.\\rst\\");
    vecGoodBye.push_back("Gute Nacht.");

    vecWrongDesicion.push_back("Du hast dich leider falsch entschieden.");
    vecWrongDesicion.push_back("Diese Karte kommt nicht jetzt.");
    vecWrongDesicion.push_back("Leider falsch. Denke noch einmal nach.");
    vecWrongDesicion.push_back("Leider falsch. Schaue dir noch einmal die anderen Karten an.");
    vecWrongDesicion.push_back("Da ist dir leider ein Fehler unterlaufen.");

    vecGoodDesicion.push_back("Diese Karte ist richtig. ");
    vecGoodDesicion.push_back("Du bist unschlagbar, diese Entscheidung ist richtig. ");
    vecGoodDesicion.push_back("Juuuhuuu! Richtige Entscheidung. ");
    vecGoodDesicion.push_back("Das war die richtige Entscheidung. ");
    vecGoodDesicion.push_back("Du hast den Nagel auf den Kopf getroffen. ");
    vecGoodDesicion.push_back("Das ist richtig! Weiter so!");
}

/**
 * destructor
 */
SpeechHelper::~SpeechHelper()
{
}

void SpeechHelper::sayStringMessage(const string message)
{
    try
    {
        // get the ALTextToSpeechProxy
        AL::ALTextToSpeechProxy fTtsProxy = AL::ALTextToSpeechProxy();
        fTtsProxy.say(message);
    } catch (const AL::ALError& e)
    {
        qiLogError("SpeechModule::sayStringMessage ") << e.what() << std::endl;
    }
}

/**
 * method to say all static speeches
 * @param text_number the number of the text the NAO should say
 */
void SpeechHelper::sayWithoutChoice(const int &text_number)
{
    try
    {
        // get the ALTextToSpeechProxy
        AL::ALTextToSpeechProxy fTtsProxy = AL::ALTextToSpeechProxy();
        // check, what message the nao should say
        switch (text_number)
        {
            case 1:
                // start message, NAO introduces himself
                fTtsProxy.say(beginMessage);
                break;
            case 2:
                // explain rules
                fTtsProxy.say(explanationMessage);
                break;
            case 3:
                // select module message
                fTtsProxy.say(selectModuleMessage);
                break;
            case 4:
                // select Module explanation
                fTtsProxy.say(selectModuleExplanationMessage);
        }
    } catch (const AL::ALError& e)
    {
        qiLogError("SpeechModule::sayWithChoice ") << e.what() << std::endl;
    }
}

/**
 * method to say a text, that has multiple phrases
 * @param vector_number the number of the vector, where the phrase is located
 */
void SpeechHelper::sayWithChoice(const int &vector_number)
{
    try
    {
        int random_number = 0;
        AL::ALTextToSpeechProxy fTtsProxy = AL::ALTextToSpeechProxy();
        string moveCall;
        stringstream sstr;
        char number = '_';
        const char *number_string;
        switch (vector_number)
        {
            case SPEECH_GOODDESICION: //2 NAO reacts to a good desicion
                random_number = (std::rand()%vecGoodDesicion.size());
                fTtsProxy.say(vecGoodDesicion[random_number]);
                break;
            case SPEECH_WIN: //3 Player wins the game
                random_number = (std::rand()%vecWin.size());
                fTtsProxy.say(vecWin[random_number]);
                break;
            case SPEECH_GOODBYE: //6 NAO says good bye to the user
                random_number = (std::rand()%vecGoodBye.size());
                fTtsProxy.say(vecGoodBye[random_number]);
                break;
            case SPEECH_WRONGDESICION: //7 user has set wrong
                random_number = (std::rand()%vecWrongDesicion.size());
                fTtsProxy.say(vecWrongDesicion[random_number]);
                break;
        }
    } catch (const AL::ALError& e)
    {
        qiLogError("SpeechModule::sayWithChoice ") << e.what() << std::endl;
    }
}

/**
 * method to cancel the current speech
 */
void SpeechHelper::cancelSpeech()
{
    try
    {
        AL::ALTextToSpeechProxy fTtsProxy = AL::ALTextToSpeechProxy();
        fTtsProxy.stopAll();
    } catch (const AL::ALError& e)
    {
        qiLogError("SpeechModule::sayWithChoice ") << e.what() << std::endl;
    }
}
