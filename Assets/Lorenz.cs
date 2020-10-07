using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lorenz : MonoBehaviour
{
    double sig = 10.0;
    double rho = 28.0;
    double bet = 8.0 / 3.0;
    int n = 3;
    double[] x;
    double[] xprime;
    double[] xstore;
    double[] k1;
    double[] k2;
    double[] k3;
    double[] k4;
    public Color color = Color.blue;
    // Start is called before the first frame update
    void Start()
    {

        x = new double[n];
        x[0] = transform.position.x;
        x[1] = transform.position.y;
        x[2] = transform.position.z;
        xprime = new double[n];
        xstore = new double[n];
        k1 = new double[n];
        k2 = new double[n];
        k3 = new double[n];
        k4 = new double[n];
        GetComponent<Renderer>().material.color = color;
        GetComponent<TrailRenderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            (float)x[0], (float)x[1], (float)x[2]
        );
    }

    void RatesOfChange(double[] xin)
    {
        double x = xin[0];
        double y = xin[1];
        double z = xin[2];
        xprime[0] = sig * (y - x);
        xprime[1] = x * (rho - z) - y;
        xprime[2] = x * y - bet * z;
    }

    private void FixedUpdate()
    {
        double h = (double)Time.fixedDeltaTime;
        RatesOfChange(x); // start using current position
        for (int i = 0; i < n; i++)
        {
            k1[i] = xprime[i] * h;
            xstore[i] = x[i] + 0.5 * k1[i];
        }
        RatesOfChange(xstore);
        for (int i = 0; i < n; i++)
        {
            k2[i] = xprime[i] * h;
            xstore[i] = x[i] + 0.5 * k2[i];
        }
        RatesOfChange(xstore);
        for (int i = 0; i < n; i++)
        {
            k3[i] = xprime[i] * h;
            xstore[i] = x[i] + k3[i];
        }
        RatesOfChange(xstore);
        for (int i = 0; i < n; i++)
        {
            k4[i] = xprime[i] * h;
            x[i] = x[i] + (1.0 / 6.0) *
                (k1[i] + 2.0 * k2[i] +
                2.0 * k3[i] + k4[i]);
        }
    }
}
