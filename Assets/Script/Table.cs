using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    public Text text;

    private LineRenderer liney; //Grapf
        
    public void CalcLagranj()//Полином Лагранджа
    {
        text.text = "";

        float[] x = new float[5];
        float[] y = new float[5];

        GameObject[] Points = GameObject.FindGameObjectsWithTag("Point");

        foreach (var item in Points)
        {
            x[item.GetComponent<Point>().indx - 1] = item.transform.position.x;
            y[item.GetComponent<Point>().indx - 1] = item.transform.position.y;
        }

        //MNK(x, y);
        float[] lag = new float[7];
        float[] xW = RetSix(x);
        lag[0] = Lagranj(x, y, 0);
        lag[6] = Lagranj(x, y, 6);

        //for (int i = 0; i < x.Length; i++)
        //{
        //    lag[i + 1] = y[i];

        //}
        for (int i = 0; i < x.Length; i++)
        {
            lag[i + 1] = Lagranj(x, y, x[i]);

        }

        LineRend(xW.Length, xW, lag, Color.red);
        
        for (int i = 0; i < lag.Length; i++)
        {
            text.text += "X: " + xW[i] + " F(x): " + lag[i] + "\n";
        }
    }

    public void CalMNK()//метод найменших квадратов
    {
        float[] x = new float[5];
        float[] y = new float[5];

        GameObject[] Points = GameObject.FindGameObjectsWithTag("Point");

        foreach (var item in Points)
        {
            x[item.GetComponent<Point>().indx - 1] = item.transform.position.x;
            y[item.GetComponent<Point>().indx - 1] = item.transform.position.y;
        }

        MNK(x, y);
        
    }
    
    public void Clear()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    /// <summary>
    ///  Методом наименьших квадратов
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void MNK(float[] x, float[] y)
    {
        float[] xiyi = new float[x.Length];
        float[] Xpow = new float[y.Length];

        for (int i = 0; i < x.Length; i++)
        {
            xiyi[i] = x[i] * y[i];
            Xpow[i] = Mathf.Pow(x[i], 2);
        }

        float sumX = x.Sum();           //sum X
        float sumY = y.Sum();           //sum Y
        float sumxiyi = xiyi.Sum();     //sum XiYi
        float sumXpow = Xpow.Sum();     //sum x^2


        int N = x.Length;                                   //count numb

        float a = (N * sumxiyi - sumX * sumY) / (N * sumXpow - Mathf.Pow(sumX, 2));
        float b = (sumY - (a * sumX)) / N;

        float[] iterpolar = new float[7];                   //mass interpolar numb
        float[] xW = RetSix(x);

        for (int i = 0; i < xW.Length; i++)
        {
            iterpolar[i] = a * xW[i] + b;
        }

        LineRend(xW.Length, xW, iterpolar, Color.black);        //Grapf

        text.text = "";
        for (int i = 0; i < iterpolar.Length; i++)
        {
            text.text += "X: " + xW[i] + " F(x): " + iterpolar[i] + "\n";
        }
    }


    float Lagranj(float[] x, float[] y, float xf)//xf для нужного х
    {
        float l = 0;

        float[] L = new float[x.Length]; //L{x}

        L[0] = ((xf - x[1]) * (xf - x[2]) * (xf - x[3]) * (xf - x[4])) /
           ((x[0] - x[1]) * (x[0] - x[2]) * (x[0] - x[3]) * (x[0] - x[4]));

        L[1] = ((xf - x[0]) * (xf - x[2]) * (xf - x[3]) * (xf - x[4])) /
           ((x[1] - x[0]) * (x[1] - x[2]) * (x[1] - x[3]) * (x[1] - x[4]));

        L[2] = ((xf - x[0]) * (xf - x[1]) * (xf - x[3]) * (xf - x[4])) /
               ((x[2] - x[0]) * (x[2] - x[1]) * (x[2] - x[3]) * (x[1] - x[4]));

        L[3] = ((xf - x[0]) * (xf - x[1]) * (xf - x[3]) * (xf - x[4])) /
               ((x[3] - x[0]) * (x[3] - x[1]) * (x[3] - x[2]) * (x[2] - x[4]));

        L[4] = ((xf - x[0]) * (xf - x[1]) * (xf - x[2]) * (xf - x[3])) /
               ((x[4] - x[0]) * (x[4] - x[1]) * (x[4] - x[2]) * (x[4] - x[3]));

        
        
        for (int i = 0; i < L.Length; i++)
        {
            l += L[i] * y[i];            
        }
        
        return l;
    }

    void LineRend(int count, float[] x, float[] y, Color col)
    {
        liney = GetComponent<LineRenderer>();

        liney.SetVertexCount(count);                          //count index
        liney.SetColors(col, col);                            //line color

        for (int i = 0; i < count; i++)
        {
            liney.SetPosition(i, new Vector3(x[i], y[i], 0));
        }
    }

    float[] RetSix(float[] x)       // x = 0, x = 6
    {
        float[] xW = new float[7];                         //x with x = { 0, 6 }

        for (int i = 0; i < x.Length; i++)
        {
            xW[i + 1] = x[i];
        }

        xW[0] = 0;                                  //x = 0
        xW[x.Length + 1] = 6;                       //x = 6

        return xW;
    }


}
