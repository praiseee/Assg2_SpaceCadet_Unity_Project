using UnityEngine;
using UnityEngine.UI;

public class MoonSample : MonoBehaviour
{
    public Slider moonSlider;
    public int moonCurrent;  
    public int moonTotal = 10; 
    public int moonIncreaseAmount = 1;

    void Start()
    {
        moonCurrent = 0;
        moonSlider.maxValue = moonTotal;
        moonSlider.value = moonCurrent;
    }

    public void CollectSample()
    {
        moonCurrent += moonIncreaseAmount;
        moonCurrent = Mathf.Clamp(moonCurrent, 0, moonTotal);
        moonSlider.value = moonCurrent;

        Debug.Log("Moon Sample Collected!");

        Destroy(gameObject);
    }
}
