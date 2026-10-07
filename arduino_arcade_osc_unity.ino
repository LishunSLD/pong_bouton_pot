#include <Arduino.h>

#include <Bounce2.h>
Bounce2::Button button1;

#include <Chrono.h>
Chrono chronoPot;

#include <MicroOscSlip.h>
MicroOscSlip<128> monOsc(&Serial); 

void setup()
{
    Serial.begin(115200);

    button1.attach(2, INPUT_PULLUP);
    button1.setPressedState(LOW);
    pinMode(3, OUTPUT);
    pinMode(PIN_A2, INPUT);
}

void loop()
{
    button1.update();


    if (button1.pressed()) {

        monOsc.sendInt("/but0", 1);
    }

    if (button1.released()) {

        monOsc.sendInt("/but0", 0);
    }

    if (button1.isPressed()) {
        digitalWrite( 3 , HIGH );
    } else {
        digitalWrite( 3 , LOW );
    }

    if ( chronoPot.hasPassed(20)) { // SI LE CHRONO DÉPASSE 20 MILLISECONDES
    chronoPot.restart(); // REPARTIR LE CHRONO

    int valeur = analogRead(PIN_A2); // LECTURE DE LA TENSION ENTRE 0 ET 1023

    monOsc.sendInt("/pot", valeur); // ENVOYER LA VALEUR
    }
}
