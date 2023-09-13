/**
 * @file water_consumption_objects.cpp
 * @author Niklas Schmidt, Jenny Dietrich, Felix Heilmann
 * @date 09.06.2023
 * @brief Implementation of Game objects.
 */

#include "game_object.h"

/**
 * @brief Implementation of one game object
 */
class Egg : public GameObject
{
public:
    Egg() {}

    std::string getId()
    {
        return "\"eggs\"";
    }

    std::string getName()
    {
        return "Eier";
    }

    std::string getDescription()
    {
        return "Ein Kilogramm Eier verbraucht während der Herstellung 3300 Liter. Die Hühner, die die Eier legen, trinken Wasser, um gesund zu bleiben und gute Eier zu legen. Die Eier werden gewaschen, um sie sauber zu machen. Dafür wird Wasser verwendet, um Schmutz und andere Dinge von den Eiern wegzuspülen. Manche Eier werden gekühlt, um sie frisch zu halten. Dafür werden sie in Kühlschränken oder Kühlmaschinen mit Wasser gekühlt. Manchmal werden Eier zu anderen leckeren Sachen wie Nudeln oder Kuchen verarbeitet. Dabei kann Wasser verwendet werden, um die Zutaten zu mischen.";
    }

    int getWeight()
    {
        return 3;
    }
};

/**
 * @brief Implementation of one game object
 */
class Tomato : public GameObject
{
public:
    Tomato() {}

    std::string getId()
    {
        return "\"tomato\"";
    }

    std::string getName()
    {
        return "Tomate";
    }

    std::string getDescription()
    {
        return "Ein Kilogramm Tomaten verbraucht während der Herstellung 110 Liter. Wasser ist auch sehr wichtig für Tomaten. Die Pflanzen, die Tomaten wachsen lassen, brauchen Wasser, um groß und lecker zu werden. Nachdem die Tomaten geerntet wurden, werden sie oft gewaschen, um Schmutz und Dreck abzuspülen. Manchmal werden aus den Tomaten auch leckere Sachen wie Tomatensauce oder Ketchup gemacht. Dafür werden sie zerkleinert und Wasser hilft dabei, die Tomaten zu einer Soße zu machen. Beim Anbau von Tomaten in Gewächshäusern ist es auch wichtig, dass sie genug Wasser bekommen.";
    }

    int getWeight()
    {
        return 1;
    }
};

/**
 * @brief Implementation of one game object
 */
class Apple : public GameObject
{
public:
    Apple() {}

    std::string getId()
    {
        return "\"apple\"";
    }

    std::string getName()
    {
        return "Apfel";
    }

    std::string getDescription()
    {
        return "Ein Kilogramm Äpfel verbraucht während der Herstellung 700 Liter. Wasser ist sehr wichtig, wenn wir Äpfel machen. Die Bäume, die Äpfel wachsen lassen, brauchen Wasser, um groß und gesund zu werden. Nachdem die Äpfel gepflückt wurden, werden sie gewaschen, um Schmutz und Dreck wegzuspülen. Manchmal werden die Äpfel zu Saft oder Apfelmus gemacht. Dabei werden sie zerkleinert und Wasser hilft, den Saft oder Mus herauszubekommen. Auch beim Lagern der Äpfel wird Wasser verwendet, um sicherzustellen, dass sie frisch bleiben.";
    }

    int getWeight()
    {
        return 2;
    }
};

/**
 * @brief Implementation of one game object
 */
class Cacao : public GameObject
{
public:
    Cacao() {}

    std::string getId()
    {
        return "\"cacao\"";
    }

    std::string getName()
    {
        return "Kakao";
    }

    std::string getDescription()
    {
        return "Ein Kilogramm Kakao verbraucht während der Herstellung 27000 Liter. Bei der Herstellung von Kakao wird Wasser für verschiedene Zwecke verwendet. Es wird verwendet, um die Kakao-Pflanzen zu bewässern, damit sie wachsen können. Viele Kakaopflanzen werden auf großen Feldern angebaut, bei denen es keinen Sonnenschutz gibt. Deshalb muss noch mehr Wasser verwendet werden. Wasser hilft auch dabei, den leckeren Geschmack der Kakaobohnen während der besonderen Reifung zu entwickeln. Es wird verwendet, um die Bohnen zu reinigen und von Schmutz zu befreien. Außerdem wird Wasser bei mehreren Schritten der Weiterverarbeitung zu Schokolade verwendet.";
    }

    int getWeight()
    {
        return 5;
    }
};

/**
 * @brief Implementation of one game object
 */
class Nuts : public GameObject
{
public:
    Nuts() {}

    std::string getId()
    {
        return "\"nuts\"";
    }

    std::string getName()
    {
        return "Nüsse";
    }

    std::string getDescription()
    {
        return "Ein Kilogramm Nüsse verbraucht während der Herstellung 5000 Liter. Wasser wird bei der Produktion von Nüssen für vier Dinge verwendet: Erstens, um die Nussbäume zu gießen, damit sie gut wachsen können. Zweitens, um die geernteten Nüsse zu reinigen und von Schmutz zu befreien. Drittens, um die Nüsse zu trocknen, damit sie länger haltbar sind. Und viertens, um die Nüsse zu verarbeiten, zum Beispiel zu Nussbutter oder Nussmilch.";
    }

    int getWeight()
    {
        return 4;
    }
};
