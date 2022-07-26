using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human
{
    public enum DateFormat { numeric, written, numericShortYear }

    public string name;
    public string firstName;

    public int bornYear;
    public int bornMonth;
    public int bornDay;

    public int sex; // 1: male, 2: female, 3: other

    public Human()
    {
        if (Random.value < GameManager.instance.otherGenderProbability) sex = 3;
        else if (Random.value < 0.5) sex = 1;
        else sex = 2;

        firstName = GetRandomFistName(sex);
        name = GetRandomName();

        GetRandomBirth(ref bornYear, ref bornMonth, ref bornDay);
    }

    public string FullName 
    {
        get => $"{firstName} {name.ToUpper()}";
    }

    public string GetFormattedBirth(DateFormat format)
    {
        return format switch
        {
            DateFormat.numeric => $"{bornDay} {monthName[bornMonth]} {bornYear}",
            DateFormat.written => $"{GetNumberText(bornDay, 2)}/{GetNumberText(bornMonth, 2)}/{bornYear}",
            DateFormat.numericShortYear => $"{GetNumberText(bornDay, 2)}/{GetNumberText(bornMonth, 2)}/{GetNumberText(bornYear, 2)}",
            _ => throw new System.ArgumentException(),
        };
    }

    public static string GetNumberText(int val, int digitCount)
    {
        string txt = val.ToString();

        if (txt.Length < digitCount)
        {
            return txt.PadLeft(digitCount, '0');
        }
        else if (txt.Length > digitCount)
        {
            return txt[^(digitCount+1)..];
        }
        else
        {
            return txt;
        }
    }

    public static string GetRandomFistName(int sex)
    {
        bool male;
        if (sex == 1) male = true;
        else if (sex == 2) male = false;
        else male = Random.value > 0.5;

        if (male)
            return maleNames[Random.Range(0, maleNames.Length)];
        else
            return femaleNames[Random.Range(0, femaleNames.Length)];
    }

    public static string GetRandomName()
    {
        return names[Random.Range(0, names.Length)]; ;
    }

    public static void GetRandomBirth(ref int year, ref int month, ref int day)
    {
        year = Random.Range(GameManager.instance.birthYearRange.start, GameManager.instance.birthYearRange.end);
        month = Random.Range(0, 12);
        day = Random.Range(1, daysInMonth[month] + 1);
    }
    
    public static string[] femaleNames = 
    {
        "Nathalie","Isabelle","Marie","Sylvie","Valérie","Sandrine","Stéphanie","Catherine","Sophie",
        "Céline","Véronique","Christine","Aurélie","Virginie","Julie","Christelle","Laurence","Corinne",
        "Elodie","Emilie","Caroline","Patricia","Anne","Audrey","Florence","Laetitia","Delphine","Karine",
        "Cécile","Mélanie","Laura",
    };

    public static string[] maleNames = 
    {
        "Christophe","Nicolas","Philippe","Frédéric","Stéphane","Sébastien","David","Laurent","Eric","Julien",
        "Olivier","Thierry","Pascal","Alexandre","Jean","Thomas","Vincent","Guillaume","Pierre","Patrick",
        "Jerome","Franck","Bruno","Anthony","Romain","Alain","Didier","Maxime","Kevin","François","Michel",
    };

    public static string[] names =
    {
        "Martin","Bernard","Thomas","Petit","Robert","Richard","Durand","Dubois","Moreau","Laurent",
        "Simon","Michel","Lefebvre","Leroy","Roux","David","Bertrand","Morel","Fournier","Girard",
        "Bonnet","Dupont","Lambert","Fontaine","Rousseau","Vincent","Muller","Lefevre","Faure","Andre",
        "Mercier","Blanc","Guerin","Boyer","Garnier","Chevalier","Francois","Legrand","Gauthier","Garcia",
        "Perrin","Robin","Clement","Morin","Nicolas","Henry","Roussel","Mathieu","Gautier","Masson",
        "Marchand","Duval","Denis","Dumont","Marie","Lemaire","Noel","Meyer","Dufour","Meunier",
        "Brun","Blanchard","Giraud","Joly","Riviere","Lucas","Brunet","Gaillard","Barbier","Arnaud",
        "Martinez","Gerard","Roche","Renard","Schmitt","Roy","Leroux","Colin","Vidal","Caron",
        "Picard","Roger","Fabre","Aubert","Lemoine","Renaud","Dumas","Lacroix","Olivier","Philippe",
        "Bourgeois","Pierre","Benoit","Rey","Leclerc","Payet","Rolland","Leclercq","Guillaume","Lecomte",
    };

    public static int[] daysInMonth =
    {
        31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
    };

    public static string[] monthName =
    {
        "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septempnre", "Octobre", "Novembre", "Décembre",
    };
}
