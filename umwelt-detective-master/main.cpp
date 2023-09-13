/**
 * @file main.cpp
 * @author Niklas Schmidt, Jenny Dietrich, Felix Heilmann
 * @date 09.06.2023
 * @brief Main method for the game.
 */

#include "game.h"
#include <boost/shared_ptr.hpp>
#include <alcommon/albroker.h>
#include <alcommon/albrokermanager.h>

#ifdef _WIN32
#define ALCALL __declspec(dllexport)
#else
#define ALCALL
#endif

extern "C"
{
    ALCALL int _createModule(boost::shared_ptr<AL::ALBroker> broker)
    {
        // init broker
        AL::ALBrokerManager::setInstance(broker->fBrokerManager.lock());
        AL::ALBrokerManager::getInstance()->addBroker(broker);

        // create module instances
        AL::ALModule::createModule<THWildUmweltDetektiveModule>(broker, "THWildUmweltDetektiveModule");
        AL::ALMemoryProxy almp = AL::ALMemoryProxy(broker);
        almp.subscribeToEvent("RightBumperPressed", "THWildUmweltDetektiveModule", "startGame");
        return 0;
    }

    ALCALL int _closeModule()
    {
        return 0;
    }
} // extern "C"
