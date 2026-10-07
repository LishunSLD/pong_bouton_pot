using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;


public class OscProcess : MonoBehaviour

{
    public extOSC.OSCReceiver oscReceiver;
    public PlayerPaddle player;
    public Ball ball;
    public GameManager gamemanager;

    public int potInMin = 0;
    public int potInMax = 1023;
    public float potOutMin = 0.0f;
    public float potOutMax = 1.0f;


    void Start()
    {
        oscReceiver.Bind("/but0", TraiterMessagebutton0);
        oscReceiver.Bind("/pot", TraiterPot);
    }

    // Update is called once per frame

    void TraiterMessagebutton0(OSCMessage message)
{
    // Validez qu’il y a bien le nombre attendu d’arguments (1 dans l’exemple) :
    if (message.Values.Count != 1)
    {
        Debug.Log("Le message " + message.Address  + " n'a pas le bon nombre d’arguments");
        return; // Quitte la fonction sans exécuter la suite
    }

    // Vérifiez que l’argument est du type attendu (`int` dans l’exemple) :
    if (message.Values[0].Type != OSCValueType.Int)
    {
        Debug.Log("Le premier argument du message " + message.Address  + "n'est pas un entier");
        return; // Quitte la fonction sans exécuter la suite
    }

    // Récupérer la valeur de l’argument :
    int valeur = message.Values[0].IntValue;

    // Deboguer
    Debug.Log("Reçu : " + message.Address + " " + valeur);

    // FAIRE DE QUOI AVEC LA VARIABLE VALEUR ICI !
    if ( valeur == 1 ) {
            gamemanager.ThrowBall();
    } else {

    }
}

    void TraiterPot(OSCMessage message){
        int valeur = message.Values[0].IntValue;
        float ajuste = (((float)valeur - potInMin) / (potInMax - potInMin) * (potOutMax - potOutMin) + potOutMin);

        player.SetPosition(ajuste);
        Debug.Log(valeur);
    }

}
