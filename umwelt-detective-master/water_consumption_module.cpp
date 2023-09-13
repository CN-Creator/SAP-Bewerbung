/**
 * @file water_consumption_module.cpp
 * @author Niklas Schmidt, Jenny Dietrich, Felix Heilmann
 * @date 09.06.2023
 * @brief Implementation of Game module.
 */

#include "game_module.h"
#include "water_consumption_objects.cpp"

class WaterConsumptionModule : public GameModule
{
public:
    /**
     * @brief Destructor of the WaterConsumptionModule
     */
    ~WaterConsumptionModule() {}

    /**
     * @brief Get the Id of the object module
     * @return std::string
     */
    std::string getId()
    {
        return "water_consumption";
    }

    /**
     * @brief Get the Name of the game module
     * @return std::string
     */
    std::string getName()
    {
        return "Wasserverbrauch in der Lebensmittelherstellung. ";
    }

    /**
     * @brief Get the Question of the game module
     *
     * @return std::string
     */
    std::string getQuestion()
    {
        return "Welches Lebensmittel verbraucht am meisten Wasser in der Herstellung? "
               "Beginne mit der Karte, welche am wenigsten Wasserverbrauch hat. ";
    }

    /**
     * @brief Get the Module Introduction
     * @return std::string
     */
    std::string getModuleIntroduction()
    {
        return "Warum ist das Einsparen von Wasser so wichtig für den Umweltschutz? Das Einsparen von Wasser ist wichtig für Umweltschutz und Nachhaltigkeit. Es gibt begrenzte Wasserressourcen auf der Erde, daher ist es wichtig, Wasser zu sparen, damit genug für alle Menschen, Tiere und Pflanzen vorhanden ist. Wasser sparen hilft auch, Energie zu sparen und den Einsatz von fossilen Brennstoffen zu reduzieren. Es schützt die Lebensräume von Tieren und Pflanzen in Gewässern und verringert die Menge an Abwasser, das behandelt werden muss.";
    }

    /**
     * @brief Get sustainability tips of the game module
     * @return std::string
     */
    std::string getSustainabilityTips()
    {
        return "Kurz duschen! Versuche, schneller zu duschen und stelle dabei einen lustigen Timer, wie eine Sanduhr oder einen Timer auf deinem Telefon. So sparst du viel Wasser! Wasserhahn zu! Achte darauf, den Wasserhahn richtig zuzudrehen, wenn du ihn nicht mehr brauchst. So verschwendest du kein Wasser. Regenwasser nutzen! Stelle einen Eimer oder eine Regentonne draußen auf, wenn es regnet, und verwende das gesammelte Regenwasser zum Gießen deiner Pflanzen. Das ist kostenlos und spart Wasser!";
    }

    /**
     * @brief Get the game objects of the game module
     * @return std::vector<GameObject*>
     */
    std::vector<GameObject *> getObjects()
    {
        std::vector<GameObject *> gObjects;
        gObjects.push_back(new Egg());
        gObjects.push_back(new Tomato());
        gObjects.push_back(new Cacao());
        gObjects.push_back(new Nuts());
        gObjects.push_back(new Apple());
        return gObjects;
    }
};
