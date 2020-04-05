using UnityEngine;
using RootMotion.Dynamics;

public class EnemyPuppetController : MiniEnemyAI
{
    public PuppetMaster puppetMaster;
    public PuppetMaster.StateSettings stateSettings = PuppetMaster.StateSettings.Default;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        MyTransform = MyAgent.transform;
    }

    public override void Fall()
    {
        base.Fall();

        MyAgent.speed = 0;

        if (MyAgent.enabled)
            MyAgent.SetDestination(MyTransform.position);

        puppetMaster.Kill(stateSettings);
    }

    public override void StandUp()
    {
        base.StandUp();

        MyAgent.speed = moveSpeed;

        if (MyAgent.enabled)
            MyAgent.SetDestination(target.transform.position);

        puppetMaster.Resurrect();
    }

    public override void Die()
    {
        MyAgent.speed = 0;
        if (MyAgent.enabled)
            MyAgent.SetDestination(MyTransform.position);
        MyAgent.enabled = false;
        gameObject.tag = "Dead";
        MyAgent.tag = "Dead";
        //Counter.Kills++;
        ///////////////////////////////////////////////////////////////////////////////////////////////// НЕ ГАВНОКОД, А ГАВНОКОДИЩЕ!!!!!!!!!!!!!!!!!!!!!!

        puppetMaster.Kill(stateSettings);

        //Counter.Kills++;
        Variables.Score += Score;
        ///////////////////////////////////////////////////////////////////////////////////////////////// НЕ ГАВНОКОД, А ГАВНОКОДИЩЕ!!!!!!!!!!!!!!!!!!!!!!
        GameLevel HP = GameObject.Find("Scripts").GetComponent<GameLevel>(); //Общие жизни
        Variables.KillsPerSession++;
        HP.SetCountText();
        //Variables.Money += Money;
        Variables.Grow_Exp(Score * (int)StartSpeed);
        //Variables.MiniSummEnemies(Convert.ToString(TypeOfEnemy));

        // спавн предметов после смерти
        EnemyDrop enemyDrop = GetComponent<EnemyDrop>();
        //if (enemyDrop != null) enemyDrop.dropItems();
        //OnDieChange?.Invoke(this);
    }
}
