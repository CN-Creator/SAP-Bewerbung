/**
 * @file game_module.h
 * @author Niklas Schmidt, Jenny Dietrich, Felix Heilmann
 * @date 09.06.2023
 * @brief The description for the game module.
 */

#ifndef GAME_MODULE
#define GAME_MODULE

#include <string>
#include <vector>
#include "game_object.h"

class GameModule
{
public:
    /**
     * Destructor of Sequence
     */
    virtual ~GameModule() {}

    /**
     * @brief Get the Id of the game module
     * @return std::string
     */
    virtual std::string getId() = 0;

    /**
     * @brief Get the Name of the game module
     * @return std::string
     */
    virtual std::string getName() = 0;

    /**
     * @brief Get the Question of the game module
     * @return std::string
     */
    virtual std::string getQuestion() = 0;

    /**
     * @brief Get the Answer of the game module
     * @return std::string
     */
    virtual std::string getModuleIntroduction() = 0;

    /**
     * @brief Get sustainability tips of the game module
     * @return std::string
     */
    virtual std::string getSustainabilityTips() = 0;

    /**
     * @brief Get the game objects of the game module
     * @return std::vector<GameObject*>
     */
    virtual std::vector<GameObject *> getObjects() = 0;
};

#endif
