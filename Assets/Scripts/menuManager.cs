using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class menuManager : MonoBehaviour {
    [SerializeField] private TMP_Text mensajeResultado;



    public void activarMenu(bool valor) {
        gameObject.SetActive(valor);
    }

    public void setMensajeDerrota() {
        mensajeResultado.text = "Has perdido!";
    }
    
    public void setMensajeVictoria() {
        mensajeResultado.text = "Has ganado!";
    }
}
