using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public List<List<Field>> levelLayout;
    public List<Vector2Int> targets; // na jakich pozycjach są pola docelowe
    public int width; // dlugosc najdluzszego wiersza
    public int height; // ile wierszy

    public Level() {
        string demoLevel =
@"######
#....#
#@.$_#
#..$_#
######";
        CreateFromString(demoLevel);
    }

    public Level(string s) {
        CreateFromString(s);
    }

    private void CreateFromString(string s) {
        levelLayout = new List<List<Field>>();
        // # - sciana, . - podloga, _ - pole docelowe, $ - skrzynia, @ - gracz
        List<Field> row = new List<Field>();
        foreach (char c in s)
        {
            switch (c)
            {
                
                case '#':
                    row.Add(new Field(FloorType.Wall));
                    break;
                case '.':
                    row.Add(new Field(FloorType.Floor));
                    break;
                case '_':
                    row.Add(new Field(FloorType.Target));
                    break;
                case '$':
                    row.Add(new Field(FloorType.Floor, Entity.Crate));
                    break;
                case '@':
                    row.Add(new Field(FloorType.Floor, Entity.Player));
                    break;
                case '\n':
                    levelLayout.Add(row);
                    if (row.Count > width) width = row.Count;
                    row = new List<Field>();
                    break;
            }
        }
        levelLayout.Add(row);
        if (row.Count > width) width = row.Count;
        row = new List<Field>();


        height = levelLayout.Count;
        //TODO uzupelnic levelLayout (i pozostałe zmienne)
        Debug.Log(height + " " + width);
        Debug.Log("Poziom stworzony.");
    }
}

