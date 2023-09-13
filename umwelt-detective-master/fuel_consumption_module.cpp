#include "game_module.h"
#include "fuel_consumption_objects.cpp"

class FuelConsumptionModule : public GameModule {
public:
    ~FuelConsumptionModule(){}

    std::string getId() {
        return "fuel_consumption";
    }

    std::string getName() {
        return "Spritverbrauch von Transportmitteln. ";
    }

    std::string getQuestion() {
        return "Welches der Transportmittel verbraucht am meisten Kraftstoff? "
               "Beginne mit der Karte, welche am wenigsten Kraftstoffverbrauch hat. ";
    }

    std::string getModuleIntroduction() {
        return "Das ist ein Test Modul zu Präsentationszwecken.";
    }

    std::string getSustainabilityTips() {
        return "Laufen for the win";
    }

    std::vector<GameObject*> getObjects() {
        std::vector<GameObject*> gObjects;
        gObjects.push_back(new Car());
        gObjects.push_back(new Plane());
        gObjects.push_back(new Bus());
        gObjects.push_back(new Bike());
        gObjects.push_back(new LKW());
        return gObjects;
    }
};
