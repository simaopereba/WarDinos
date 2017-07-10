﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attributes{ATK, VIDA, VEL_ATK, VEL_DES, HAB}

public class Player : MonoBehaviour {
    static int MAX_RECURSOS = 1000;

    private int vida = 1000;
    private int recursos = 300;

    // Objetos que sao os dinossauros a serem instanciados para os grupos do jogador
    public GameObject goVelociraptor;
    public GameObject goEstegossauro;
    public GameObject goApatossauro;
    public GameObject goPterodactilo;
    public GameObject goTriceratopo;
    public GameObject goTrex;

    private Dinossauro[] goDinos = new Dinossauro[7];

    void Start()
    {
        goDinos[(int)GroupController.DinoType.APATOSSAURO] = goApatossauro.GetComponent<Apatossauro>();
        goDinos[(int)GroupController.DinoType.ESTEGOSSAURO] = goEstegossauro.GetComponent<Estegossauro>();
        goDinos[(int)GroupController.DinoType.PTERODACTILO] = goPterodactilo.GetComponent<Pterodactilo>();
        goDinos[(int)GroupController.DinoType.RAPTOR] = goVelociraptor.GetComponent<Raptor>();
        goDinos[(int)GroupController.DinoType.TREX] = goTrex.GetComponent<TRex>();
        goDinos[(int)GroupController.DinoType.TRICERATOPO] = goTriceratopo.GetComponent<Triceratopo>();
        goDinos[(int)GroupController.DinoType.NONE] = null;
    }

    public int Vida {
        get {
            return vida;
        }
    }
    // Reduz a vida por value e retorna false se jogador chegou a zero vida
    public bool reduzirVida (int value) {
        vida -= value;
        if (vida <= 0)
            return false;
        else
            return true;
    }

    public int Recursos {
        get {
            return recursos;
        }
    }
    // Reduz os recuros e retorna true se houver mais ou igual recursos que value
    // Nao reduz recursos e retorna false caso contrario
    public bool reduzirRecursos (int value) {
        if (value <= recursos) {
            recursos -= value;
            return true;
        }
        else
            return false;
    }
    
    public void incrementarRecursos (int value) {
        if (recursos < MAX_RECURSOS) {
            recursos += value;
            if (recursos > MAX_RECURSOS) {
                recursos = MAX_RECURSOS;
            }
        }
    }

    // Faz o upgrade. Retorna 1 se ocorreu ok;
    // Retorna -1, se nao tiver recursos suficiente;
    // Retorna -2, se ja esta no nivel maximo.
    // Retorna 2, se ocorreu ok, mas chegou no nivel maximo
    public int Upgrade (GroupController.DinoType dino, Attributes attr) {

        if (attr == Attributes.ATK) {
            if (reduzirRecursos(goDinos[(int)dino].CustoAttrAtaque)) {
                if (!goDinos[(int)dino].UpgradeAtaque())
                {
                    incrementarRecursos(goDinos[(int)dino].CustoAttrAtaque);
                    return -2;
                }
                else if (goDinos[(int)dino].CustoAttrAtaque > goDinos[(int)dino].GET_MAX_ATTR_ATAQUE)
                    return 2;
            }
            else return -1;
        }
        else if (attr == Attributes.VIDA) {
            if (reduzirRecursos(goDinos[(int)dino].CustoAttrVida)) {
                if (!goDinos[(int)dino].UpgradeVida()) {
                    incrementarRecursos(goDinos[(int)dino].CustoAttrVida);
                    return -2;
                }
                else if (goDinos[(int)dino].CustoAttrVida > goDinos[(int)dino].GET_MAX_ATTR_VIDA)
                    return 2;
            }
            else return -1;
        }
        else if (attr == Attributes.VEL_ATK) {
            if (reduzirRecursos(goDinos[(int)dino].CustoAttrVelocidadeAtaque)) {
                if (!goDinos[(int)dino].UpgradeVelAtq()) {
                    incrementarRecursos(goDinos[(int)dino].CustoAttrVelocidadeAtaque);
                    return -2;
                }
                else if (goDinos[(int)dino].CustoAttrVelocidadeAtaque > goDinos[(int)dino].GET_MAX_ATTR_VEL_ATQ)
                    return 2;
            }
            else return -1;
        }
        else if (attr == Attributes.VEL_DES) {
            if (reduzirRecursos(goDinos[(int)dino].CustoAttrVelocidadeDeslocamento)) {
                if (!goDinos[(int)dino].UpgradeVelDes()) {
                    incrementarRecursos(goDinos[(int)dino].CustoAttrVelocidadeDeslocamento);
                    return -2;
                }
                else if (goDinos[(int)dino].CustoAttrVelocidadeDeslocamento > goDinos[(int)dino].GET_MAX_ATTR_VEL_DES)
                    return 2;
            }
            else return -1;
        }
        else if (attr == Attributes.HAB)
        {
            if (reduzirRecursos(goDinos[(int)dino].AbilityCost))
            {
                if (!goDinos[(int)dino].UpgradeAbility())
                {
                    incrementarRecursos(goDinos[(int)dino].AbilityCost);
                    return -2;
                }
                else if (goDinos[(int)dino].HabilidadeOn)
                    return 2;
            }
            else return -1;
        }

        return 1;
    }
}
