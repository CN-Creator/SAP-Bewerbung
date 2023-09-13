/**
 * Helping Speech file
 * @author Niklas Schmidt, Jenny Dietrich, Felix Heilmann
 * Inspired by "Connect4" Project
 **/

#ifndef SPEECH_HELPER_H
#define SPEECH_HELPER_H

#include <iostream>
#include <exception>
#include <fstream>
#include <vector>
#include <algorithm>
#include <string>
#include <sstream>
#include <alcommon/albroker.h>
#include <alproxies/altexttospeechproxy.h>
#include <alproxies/alanimatedspeechproxy.h>

using namespace std;

// macros for the different speech vectors
#define SPEECH_GOODDESICION 2
#define SPEECH_WIN 3
#define SPEECH_GOODBYE 6
#define SPEECH_WRONGDESICION 7

class SpeechHelper
{
private:
  // speech vectors
  string beginMessage;
  string explanationMessage;
  string selectModuleMessage;
  string selectModuleExplanationMessage;
  vector<string> vecGoodDesicion;
  vector<string> vecWrongDesicion;
  vector<string> vecWin;
  vector<string> vecGoodBye;

public:
  /**
   * @brief Constructor of a Speech Helper object
   */
  SpeechHelper();

  /**
   * @brief Destructor of a Speech Helper object
   */
  virtual ~SpeechHelper();

  /**
   * @brief Method to let nibo say predefined message
   * @param text_number Number of the text to say
   */
  void sayWithoutChoice(const int &text_number);

  /**
   * @brief Method to let nibo say predefined message with choice
   * @param vector_number Number of the vector to say
   */
  void sayWithChoice(const int &vector_number);

  /**
   * @brief Method to let nibo say a string message
   * @param message Message to say
   */
  void sayStringMessage(const std::string message);

  /**
   * @brief Method to cancel last speech
   */
  void cancelSpeech();
};
#endif
