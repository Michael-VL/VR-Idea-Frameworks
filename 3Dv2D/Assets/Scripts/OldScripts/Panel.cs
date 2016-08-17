using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour {

    public bool start;
    public bool one;
    public bool two;
    public bool three;
    public bool four;
    public bool five;
    public bool quit;
    public bool back;

    public StartUIPanel manager;

    void OnCollisionEnter(Collision col)
    {
        if (start)
        {
            manager.Start();
        }
        else if (quit)
        {
            manager.Quit();
        }
        else if(back)
        {
            manager.Back();
        }
        else if(one)
        {
            manager.OneLeve();
        }
        else if (two)
        {
            manager.TwoLeve();
        }
        else if (three)
        {
            manager.ThreeLeve();
        }
        else if (four)
        {
            manager.FourLeve();
        }
        else if (five)
        {
            manager.FiveLeve();
        }
    }
}
