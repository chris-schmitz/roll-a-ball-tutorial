#include <SoftwareSerial.h>

SoftwareSerial sSerial(10, 11);

void setup()
{
    Serial1.begin(9600);
    sSerial.begin(9600);
}
void loop()
{
    Serial1.println(0);
    delay(1000);
    Serial1.println(1);
    delay(1000);
    sSerial.println(1);
    delay(1000);
}