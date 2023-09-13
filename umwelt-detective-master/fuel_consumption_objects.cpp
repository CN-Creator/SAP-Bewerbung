#include "game_object.h"

class Car : public GameObject {
public:
    Car(){}

    std::string getId() {
        return "\"car\"";
    }

    std::string getName() {
        return "Auto";
    }

    std::string getDescription() {
        return "";
    }

    int getWeight() {
        return 3;
    }
};

class Bike : public GameObject {
public:
    Bike(){}

    std::string getId() {
        return "\"bike\"";
    }

    std::string getName() {
        return "Fahrrad";
    }

    std::string getDescription() {
        return "";
    }

    int getWeight() {
        return 1;
    }
};

class Bus : public GameObject {
public:
    Bus(){}

    std::string getId() {
        return "\"bus\"";
    }

    std::string getName() {
        return "Bus";
    }

    std::string getDescription() {
        return "";
    }

    int getWeight() {
        return 2;
    }
};

class Plane : public GameObject {
public:
    Plane(){}

    std::string getId() {
        return "\"plane\"";
    }

    std::string getName() {
        return "Flugzeug";
    }

    std::string getDescription() {
        return "";
    }

    int getWeight() {
        return 5;
    }
};

class LKW : public GameObject {
public:
    LKW(){}

    std::string getId() {
        return "\"lkw\"";
    }

    std::string getName() {
        return "LKW";
    }

    std::string getDescription() {
        return "";
    }

    int getWeight() {
        return 4;
    }
};
