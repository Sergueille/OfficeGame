using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AvancedInfoHandler : BasicInfoHandler
{
    [SerializeField] protected TextMeshProUGUI signIdDate;
    [SerializeField] protected Human.DateFormat signInDateFormat;
    [SerializeField] protected TextMeshProUGUI familySituation;

    [SerializeField] protected BasicInfoHandler spouseHandler;

    [SerializeField] protected BasicInfoHandler childTemplate;
    [SerializeField] protected Transform noChildrenDisplay;

    public override void Refresh()
    {
        base.Refresh();

        if (signIdDate != null)
            signIdDate.text = Human.GetFormattedDate(signInDateFormat, Human.signInYear, Human.signInMonth, Human.signInDay);

        if (familySituation != null)
            familySituation.text = Human.GetFamilySituationText();

        if (spouseHandler != null)
        {
            if (Human.spouse != null)
                spouseHandler.Human = Human.spouse;
            else
                Destroy(spouseHandler.gameObject);
        }

        if (childTemplate != null)
        {
            Transform parent = childTemplate.transform.parent;

            foreach (Human human in Human.childrens)
            {
                BasicInfoHandler newChildHandler = Instantiate(childTemplate, parent);
                newChildHandler.Human = human;
            }

            if (noChildrenDisplay != null)
            {
                if (Human.childrens.Count > 0)
                    Destroy(noChildrenDisplay.gameObject);
            }

            Destroy(childTemplate.gameObject);
        }
    }
}
