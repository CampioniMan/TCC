using UnityEngine;
using System.Collections;

// Classe geral a respeito das características de monstros e do personagem
public class Caracteristicas {

    public enum Tipos
    {
        Personagem,
        Pirata,
        Pirata_Canhao,
        Arara,
        Capitao
    };

    private int vida;
    private int tipo;

    public int Vida
    {
        get
        {
            return vida;
        }

        set
        {
            vida = value;
        }
    }

    public int Tipo
    {
        get
        {
            return tipo;
        }

        set
        {
            tipo = value;
        }
    }
}
