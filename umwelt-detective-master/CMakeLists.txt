cmake_minimum_required(VERSION 2.8)
project(THWildUmweltDetektiv)
find_package(qibuild)

set("main.cpp" "main.h" "game.cpp" "game.h" "speech.cpp" "speech.h" "game_object.h" "game_module.h" "water_consumption_objects.h" "water_consumption_module.cpp" "fuel_consumption_objects.cpp" "fuel_consumption_module.cpp")

# Create a executable named THWildUmweltDetektiv
# with the source file: main.cpp
qi_create_lib(THWildUmweltDetektiv "main.cpp" "main.h" "game.cpp" "game.h" "speech.cpp" "speech.h" "game_object.h" "game_module.h" "water_consumption_objects.cpp" "water_consumption_module.cpp" "fuel_consumption_objects.cpp" "fuel_consumption_module.cpp")

qi_use_lib(THWildUmweltDetektiv ALCOMMON ALPROXIES BOOST)

# Add a simple test:
enable_testing()
qi_create_test(test_THWildUmweltDetektiv "test.cpp")
