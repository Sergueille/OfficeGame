using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human
{
    public enum DateFormat { 
        numeric, 
        written, 
        numericShortYear, 
        lastFullFormatValue = numericShortYear,
        onlyYear
    }
    public enum FamilySituation { single, married, divorced, lastValue }

    public string name;
    public string firstName;

    public int bornYear;
    public int bornMonth;
    public int bornDay;

    public bool sex; // false: male, true: female

    public string customerID;
    public int signInYear;
    public int signInMonth;
    public int signInDay;

    public FamilySituation familySituation;
    public Human spouse; 
    public List<Human> childrens;

    public Human(int forceSex = -1, string forceName = "", bool child = false)
    {
        if (forceSex == -1)
        {
            sex = Random.value < 0.5;
        }
        else sex = forceSex == 1;

        firstName = GetRandomFistName(sex);
        name = forceName != "" ? forceName : GetRandomName();

        GetRandomBirth(ref bornYear, ref bornMonth, ref bornDay, child);
        GetRandomBirth(ref signInYear, ref signInMonth, ref signInDay, true);

        childrens = new();

        if (!child)
        {
            customerID = GetRandomCustomerID();

            familySituation = (FamilySituation)Random.Range(0, (int)FamilySituation.lastValue);
            if (familySituation != FamilySituation.single)
            {
                if (Random.value > 0.5)
                    spouse = new((sex) ? 0 : 1, forceName: name);
                else
                    spouse = new((sex) ? 0 : 1);

                int childCount = Random.Range(0, GameManager.instance.maxChildCount + 1);

                string childsName;
                if (Random.value > 0.3)
                    childsName = spouse.name;
                else if (Random.value > 0.5 || name == spouse.name)
                    childsName = name;
                else if (Random.value > 0.5)
                    childsName = $"{name}-{spouse.name}";
                else
                    childsName = $"{spouse.name}-{name}";

                for (int i = 0; i < childCount; i++)
                {
                    childrens.Add(new Human(-1, childsName, true));
                }

                childrens.Sort((c1, c2) => c1.bornYear - c2.bornYear);
            }
        }
    }

    public string FullName 
    {
        get => $"{firstName} {name.ToUpper()}";
    }

    public string GetFormattedBirth(DateFormat format)
    {
        return GetFormattedDate(format, bornYear, bornMonth, bornDay);
    }

    public static string GetFormattedDate(DateFormat format, int year, int month = 1, int day = 1)
    {
        return format switch
        {
            DateFormat.numeric => $"{day} {monthName[month]} {year}",
            DateFormat.written => $"{GetNumberText(day, 2)}/{GetNumberText(month, 2)}/{year}",
            DateFormat.numericShortYear => $"{GetNumberText(day, 2)}/{GetNumberText(month, 2)}/{GetNumberText(year, 2)}",
            DateFormat.onlyYear => year.ToString(),
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
            return txt[^(digitCount)..];
        }
        else
        {
            return txt;
        }
    }

    public static string GetRandomFistName(bool sex)
    {
        if (!sex)
            return maleNames[Random.Range(0, maleNames.Length)];
        else
            return femaleNames[Random.Range(0, femaleNames.Length)];
    }

    public static string GetRandomName()
    {
        return names[Random.Range(0, names.Length)]; ;
    }

    public static void GetRandomBirth(ref int year, ref int month, ref int day, bool child = false)
    {
        if (child)
            year = Random.Range(GameManager.instance.childrensYearRange.start, GameManager.instance.childrensYearRange.end);
        else
            year = Random.Range(GameManager.instance.birthYearRange.start, GameManager.instance.birthYearRange.end);

        month = Random.Range(0, 12);
        day = Random.Range(1, daysInMonth[month] + 1);
    }

    public static string GetRandomCustomerID()
    {
        string res = "";

        for (int i = 0; i < GameManager.instance.customerIDLength; i++)
        {
            float rand = Random.value;

            if (rand < 0.5) // Number
            {
                res += Random.Range(0, 10).ToString();
            }
            else if (rand < 0.75) // Lowercase
            {
                res += (char)Random.Range('a', 'z'+1);
            }
            else // Uppercase
            {
                res += (char)Random.Range('A', 'Z' + 1);
            }
        }

        return res;
    }

    public string GetFamilySituationText()
    {
        return new string[] { "Célibataire", "Marié(e)", "Divorcé(e)" }[(int)familySituation];
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
        "Lopez","Jean","Dupuy","Guillot","Hubert","Berger","Carpentier","Sanchez","Dupuis","Moulin",
        "Louis","Deschamps","Huet","Vasseur","Perez","Boucher","Fleury","Royer","Klein","Jacquet",
        "Adam","Paris","Poirier","Marty","Aubry","Guyot","Carre","Charles","Renault","Charpentier",
        "Menard","Maillard","Baron","Bertin","Bailly","Herve","Schneider","Fernandez","Le","Collet",
        "Leger","Bouvier","Julien","Prevost","Millet","Perrot","Daniel","Le","Cousin","Germain",
        "Breton","Besson","Langlois","Remy","Le","Pelletier","Leveque","Perrier","Leblanc","Barre",
        "Lebrun","Marchal","Weber","Mallet","Hamon","Boulanger","Jacob","Monnier","Michaud","Rodriguez",
        "Guichard","Gillet","Etienne","Grondin","Poulain","Tessier","Chevallier","Collin","Chauvin","Da",
        "Bouchet","Gay","Lemaitre","Benard","Marechal","Humbert","Reynaud","Antoine","Hoarau","Perret",
        "Barthelemy","Cordier","Pichon","Lejeune","Gilbert","Lamy","Delaunay","Pasquier","Carlier","Laporte",
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
