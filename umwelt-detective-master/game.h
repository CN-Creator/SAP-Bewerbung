/**
 * @file game.h
 * @author Niklas Schmidt, Jenny Dietrich, Felix Heilmann
 * @date 09.06.2023
 * @brief The description for the game.
 * Inspired by "Connect4" Project
 */

#ifndef THWILD_UMWELT_DETEKTIVE_MODULE_H
#define THWILD_UMWELT_DETEKTIVE_MODULE_H

#include <iostream>
#include "speech.h"
#include "game_module.h"
#include <alcommon/albroker.h>
#include <alcommon/almodule.h>
#include <alproxies/almemoryproxy.h>
#include <alproxies/altexttospeechproxy.h>
#include <alproxies/alphotocaptureproxy.h>
#include <alproxies/alledsproxy.h>
#include <time.h>

namespace AL
{
  class ALBroker;
}

class THWildUmweltDetektiveModule : public AL::ALModule
{
private:
  AL::ALMemoryProxy almp;
  SpeechHelper *speech;
  GameModule *selectedModule;
  std::string qrData;
  int nextPosition;
  int neededTurns;
  std::vector<GameModule *> gameModules;
  int moduleIndex;
  bool noQRCode;
  bool gameComplete;

public:
  THWildUmweltDetektiveModule(boost::shared_ptr<AL::ALBroker> broker,
                              const std::string &name);

  /**
   * @brief Destructor of the main game module
   */
  virtual ~THWildUmweltDetektiveModule();

  /**
   * Overloading ALModule::init().
   * This is called right after the module has been loaded
   */
  virtual void init();

  /**
   * @brief Start method of the main game
   */
  void startGame();

  /**
   * @brief Explain method of the main game
   */
  void explainGame();

  /**
   * @brief End method of the main game
   */
  void endGame();

  /**
   * @brief Select Module method of the main game
   */
  void selectModule();

  /**
   * @brief Select next module of the main game
   */
  void nextModule();

  /**
   * @brief Select previous module of the main game
   */
  void previousModule();

  /**
   * @brief Get OR-Code method of the main game
   */
  void getQRCode();

  /**
   * @brief Game logic method of the main game
   */
  void gameLogic();
};
#endif
