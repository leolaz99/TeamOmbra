using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider hp;
    [SerializeField] Slider energy;
    [SerializeField] Slider dash;
    public static UIManager instance;
    
    public void HP()
    {
        hp.value = (PlayerManager.instance.life / 100f);
    }

    public void Energy()
    {
        energy.value = (PlayerManager.instance.energy / 100f);
    }

    public void Dash()
    {
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        HP();
        Energy();
    }
}
