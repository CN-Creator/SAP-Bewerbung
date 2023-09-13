/**
 * @file game_object.h
 * @author Niklas Schmidt, Jenny Dietrich, Felix Heilmann
 * @date 09.06.2023
 * @brief The description for the game object.
 */

#ifndef GAME_OBJECT
#define GAME_OBJECT

#include <string>

class GameObject
{
public:
    /**
     * @brief Destructor of the game object
     */
    virtual ~GameObject() {}

    /**
     * @brief Get the Id of the game object
     * @return std::string
     */
    virtual std::string getId() = 0;

    /**
     * @brief Get the Name of the game object
     * @return std::string
     */
    virtual std::string getName() = 0;

    /**
     * @brief Get the Description of the game object
     * @return std::string
     */
    virtual std::string getDescription() = 0;

    /**
     * @brief Get the Weight/Position of the game object
     * @return int
     */
    virtual int getWeight() = 0;
};

#endif
