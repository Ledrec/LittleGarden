using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    public GameObject[] trees;
    public Material[] terrainsMaterial;
    public Material[] skyboxMaterial;
    public Color[] treeColor;
    public Transform treeParent;
    public Terrain terrain;
    public Tree activeTree;
    public Material leavesBlue;
    public Material leavesOrange;
    public ParticleSystem moneyPrt;
    public int numberParticles;
    public float endPosZ;

    public AnimationCurve curveAnimation;
    public System.Numerics.BigInteger changeTreePrice;

    private void Awake()
    {
    }

    public void InstantiateTree(int _id)
    {
        GameObject go = Instantiate(trees[_id], Vector3.zero, Quaternion.identity, treeParent);
        activeTree = go.GetComponent<Tree>();
        changeTreePrice = (System.Numerics.BigInteger)(100000*Mathf.Pow(10,SaveManager.LoadCurrentLevel()));
        UIManager.instance.gameplayWindow.txtNextLevelPrice.text = "$" + GameManager.instance.levelManager.changeTreePrice.ToCompactString();
        ChangeScenario();
    }

    public void SellTree()
    {

        StartCoroutine(SellTreeAnimation());

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(SellTreeAnimation());
        }
    }

    public void ChangeScenario()
    {
        RenderSettings.skybox = skyboxMaterial[SaveManager.LoadCurrentLevel()];
        terrain.materialTemplate = terrainsMaterial[SaveManager.LoadCurrentLevel()];
        leavesBlue.color = treeColor[SaveManager.LoadCurrentLevel() * 2];
        leavesOrange.color = treeColor[(SaveManager.LoadCurrentLevel() * 2) + 1];
    }

    IEnumerator SellTreeAnimation()
    {
        UIManager.instance.fadeWindow.Show();
        yield return new WaitUntil(() => UIManager.instance.fadeWindow.anmtrWindow.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        Destroy(activeTree.gameObject);
        activeTree = null;
        InstantiateTree((int)Mathf.Repeat(SaveManager.LoadCurrentLevel(),trees.Length));
        moneyPrt.Emit(numberParticles);
        yield return new WaitForSeconds(.2f);
        UIManager.instance.fadeWindow.Hide();
        StartCoroutine(MoveParticles());

    }

    IEnumerator MoveParticles()
    {
        yield return new WaitForSeconds(.5f);
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[moneyPrt.particleCount];
        moneyPrt.GetParticles(particles);
        //float eTime = 0;
        //while (eTime < 2.0f)
        //{
        //    eTime += Time.deltaTime;
        //    for (int i = 0; i < particles.Length; i++)
        //    {
        //        Vector3 startPos = particles[i].position;

        //        particles[i].position = RotatePointAroundPivot(startPos, moneyPrt.transform.position, new Vector3(0, 0, Mathf.Lerp(0, -360.0f , curveAnimation.Evaluate(eTime / 2.0f)) * (Mathf.PI / 180.0f)));
        //    }
        //    moneyPrt.SetParticles(particles);
        //    yield return new WaitForEndOfFrame();
        //}
        //eTime = 0;
        //List<Vector3> startPositions = new();
        //for (int i = 0; i < particles.Length; i++)
        //{
        //    startPositions.Add(particles[i].position);
        //}
        Vector3 endPos = Camera.main.ScreenToWorldPoint(UIManager.instance.normalCurrencyCounter.transform.position);
        endPos.z = endPosZ;
        float timeForParticleToArrive = .5f;
        float eTime = 0;
        List<Vector3> startPositions = new();
        for (int i = 0; i < particles.Length; i++)
        {
            startPositions.Add(particles[i].position);
            particles[i].velocity = new Vector3();
        }
        while (eTime < timeForParticleToArrive)
        {
            eTime += Time.deltaTime;
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].position = Vector3.Lerp(startPositions[i], endPos, eTime / timeForParticleToArrive);
                particles[i].startColor = Color.Lerp(Color.white, new Color(1, 1, 1, 0), eTime / timeForParticleToArrive);
                moneyPrt.SetParticles(particles);
                if (eTime >= timeForParticleToArrive)
                {
                    particles[i].startLifetime = 0;
                    particles[i].remainingLifetime = 0;
                }
            }
            yield return new WaitForEndOfFrame();
        }
        UIManager.instance.normalCurrencyCounter.ChangeCurrency(activeTree.GetSellPrice());
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    public void GoToNextLevel()
    {
        if(SaveManager.LoadCurrency(Currency.Regular)>=changeTreePrice)
        {
            UIManager.instance.fadeWindow.midAction = () => ChangeLevel();
            UIManager.instance.fadeWindow.Show();
        }
    }

    void ChangeLevel()
    {
        UIManager.instance.normalCurrencyCounter.ChangeCurrency(-changeTreePrice);
        SaveManager.SaveCurrentLevel(SaveManager.LoadCurrentLevel() + 1);
        SellTree();
    }
}
