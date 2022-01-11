using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public RootNode GetPcBt()
    {
        List<Node> nodes = new List<Node>();

        #region Movement 
        //if not moving, checks for commander, if no commander rngpos
        LeafNode cmdDead = new LeafNode(GetComponent<PersonalMovement>().HasCommander);
        LeafNode rngPos = new LeafNode(GetComponent<PersonalMovement>().RngPos);
        List<Node> listForCommSel = new List<Node>();
        listForCommSel.Add(cmdDead);
        listForCommSel.Add(rngPos);
        Selector CommSel = new Selector(listForCommSel);
        //
        LeafNode moving = new LeafNode(GetComponent<PersonalMovement>().IsMoving);
        //Inverter invMove = new Inverter(moving);
        List<Node> listforCommSeq = new List<Node>();
        listforCommSeq.Add(moving);
        listforCommSeq.Add(CommSel);
        Sequence commSeq = new Sequence(listforCommSeq);

        LeafNode ownPos = new LeafNode(GetComponent<PersonalMovement>().OwnPos);
        LeafNode byFormation = new LeafNode(GetComponent<PersonalMovement>().PosByFormation);
        List<Node> forFormSel = new List<Node>();
        forFormSel.Add(ownPos);
        forFormSel.Add(byFormation);
        Selector formSel = new Selector(forFormSel);

        List<Node> forMovementSel = new List<Node>();
        AlwaysFalse forCommSeq = new AlwaysFalse(commSeq);
        forMovementSel.Add(forCommSeq);
        forMovementSel.Add(formSel);
        Selector moveSel = new Selector(forMovementSel);
        #endregion

        #region combat
        LeafNode inCombatRange = new LeafNode(GetComponent<PlayerAttack>().InCombatRange);
        LeafNode isRanged = new LeafNode(GetComponent<PlayerAttack>().IsRanged);
        LeafNode atkMelee = new LeafNode(GetComponent<PlayerAttack>().MeleeAttack);
        LeafNode atkRanged = new LeafNode(GetComponent<PlayerAttack>().RangeAttack);
        LeafNode lineOfSight = new LeafNode(GetComponent<PlayerAttack>().LineOfSight);
        //LeafNode aggroRange = new LeafNode(GetComponent<PlayerAttack>().WithinAggroRange);
        //LeafNode moveCloser = new LeafNode(GetComponent<PlayerAttack>().MoveTowardsEnemy);

        List<Node> forRangedSequence = new List<Node>();
        forRangedSequence.Add(isRanged);
        forRangedSequence.Add(inCombatRange);
        forRangedSequence.Add(lineOfSight);
        forRangedSequence.Add(atkRanged);
        Sequence rangedSequence = new Sequence(forRangedSequence);

        List<Node> meleeCombatIfInRange = new List<Node>();
        meleeCombatIfInRange.Add(inCombatRange);
        meleeCombatIfInRange.Add(atkMelee);
        Sequence ifInMelee = new Sequence(meleeCombatIfInRange);
        Inverter invMelee = new Inverter(ifInMelee);
        List<Node> forMeleeSequence = new List<Node>();
        //forMeleeSequence.Add(aggroRange);
        forMeleeSequence.Add(invMelee);
        forMeleeSequence.Add(lineOfSight);
        //forMeleeSequence.Add(moveCloser);
        Sequence meleeSequence = new Sequence(forMeleeSequence);
        List<Node> forCombatSelector = new List<Node>();
        forCombatSelector.Add(rangedSequence);
        forCombatSelector.Add(meleeSequence);
        Selector forCombat = new Selector(forCombatSelector);
        #endregion

        nodes.Add(moveSel);
        nodes.Add(forCombat);

        RootNode toReturn = new RootNode(nodes);
        return toReturn;
    }

    public RootNode GetEnemyBt()
    {
        List<Node> nodes = new List<Node>();

        #region Movement
        LeafNode inAggroRangeMovement = new LeafNode(GetComponent<Enemy>().InAggroRange);
        LeafNode closeToWaypoint = new LeafNode(GetComponent<WanderingEnemy>().CloseToWaypoint);
        LeafNode nextWaypoint = new LeafNode(GetComponent<WanderingEnemy>().NextWaypoint);
        List<Node> forWanderSequence = new List<Node>();
        forWanderSequence.Add(new Inverter(inAggroRangeMovement));
        forWanderSequence.Add(closeToWaypoint);
        forWanderSequence.Add(nextWaypoint);
        Sequence wanderSequence = new Sequence(forWanderSequence);
        #endregion

        #region Combat
        LeafNode inAggroRange = new LeafNode(GetComponent<Enemy>().InAggroRange);
        LeafNode melee = new LeafNode(GetComponent<Enemy>().InMeleeRange);
        LeafNode moveCloser = new LeafNode(GetComponent<Enemy>().MoveTowardsClosestPc);
        Inverter invMelee = new Inverter(melee);
        List<Node> forCombatSequence = new List<Node>();
        forCombatSequence.Add(inAggroRange);
        forCombatSequence.Add(invMelee);
        forCombatSequence.Add(moveCloser);
        Sequence combatSequence = new Sequence(forCombatSequence);
        #endregion
        
        List<Node> forGeneralSelector = new List<Node>();
        forGeneralSelector.Add(combatSequence);
        forGeneralSelector.Add(wanderSequence);
        Selector generalSelector = new Selector(forGeneralSelector);

        nodes.Add(generalSelector);

        RootNode toReturn = new RootNode(nodes);
        return toReturn;
    }

    public RootNode GetTurretBt()
    {
        List<Node> nodes = new List<Node>();
        #region turretAttack
        LeafNode canAttack = new LeafNode(GetComponent<TurretScript>().InRangeAndSight);
        LeafNode shoot = new LeafNode(GetComponent<TurretScript>().Shoot);
        List<Node> forSequence = new List<Node>();
        forSequence.Add(canAttack);
        forSequence.Add(shoot);
        Sequence sel = new Sequence(forSequence);
        #endregion

        nodes.Add(sel);

        RootNode toReturn = new RootNode(nodes);
        return toReturn;
    }

    public RootNode GetRangedEnemyBt()
    {
        //Debug.Log("tar ett Bt för ranged enemy");
        List<Node> nodes = new List<Node>();

        #region Movement
        LeafNode closeToWaypoint = new LeafNode(GetComponent<RangedEnemy>().CloseToWaypoint);
        LeafNode nextWaypoint = new LeafNode(GetComponent<RangedEnemy>().NextWaypoint);
        List<Node> forWanderSequence = new List<Node>();
        forWanderSequence.Add(closeToWaypoint);
        forWanderSequence.Add(nextWaypoint);
        Sequence wanderSequence = new Sequence(forWanderSequence);
        #endregion

        #region combat
        LeafNode rangeAndLineOfSight = new LeafNode(GetComponent<RangedEnemy>().PcInRange);
        LeafNode rangedAttack = new LeafNode(GetComponent<RangedEnemy>().RangedAttack);
        List<Node> forCombatSequence = new List<Node>();
        forCombatSequence.Add(rangeAndLineOfSight);
        forCombatSequence.Add(rangedAttack);

        LeafNode hasSeenPc = new LeafNode(GetComponent<RangedEnemy>().HasSeenPc);
        LeafNode moveToLastSeen = new LeafNode(GetComponent<RangedEnemy>().TowardsLastSeenPc);
        List<Node> forMoveTowardsSequence = new List<Node>();
        forMoveTowardsSequence.Add(hasSeenPc);
        forMoveTowardsSequence.Add(moveToLastSeen);
        Sequence combatSequence = new Sequence(forCombatSequence);
        Sequence moveTowardsSequence = new Sequence(forMoveTowardsSequence);
        List<Node> forMainCombatSelector = new List<Node>();
        forMainCombatSelector.Add(combatSequence);
        forMainCombatSelector.Add(moveTowardsSequence);
        Selector combatSelector = new Selector(forMainCombatSelector);
        #endregion

        List<Node> forGeneralSelector = new List<Node>();
        forGeneralSelector.Add(combatSelector);
        forGeneralSelector.Add(wanderSequence);
        Selector generalSelector = new Selector(forGeneralSelector);

        nodes.Add(generalSelector);

        RootNode toReturn = new RootNode(nodes);
        return toReturn;
    }
}
/// <summary>
/// startar processen, går igenom alla noder och kör deras evaluates.
/// 
/// OBS! endast de noderna som ska vara direkt kopplade till rot-noden ska vara i input-listan OCH DE KOMMER ALLA KÖRAS!
/// </summary>
public class RootNode
{
    List<Node> children = new List<Node>();
    public RootNode(List<Node> nodes)
    {
        children = nodes;
    }
    public void Start()
    {
        foreach (Node node in children)
        {
            node.Evaluate();
        }
    }
}
public abstract class Node
{
    public enum NodeStates
    {
        fail,
        success,
        running
    }
    protected NodeStates nodeState;
    public NodeStates getNodeState
    {
        get { return nodeState; }
    }
    public Node()
    {
    }
    public abstract NodeStates Evaluate();
}
/// <summary>
/// Tar en lista av nodes, itererar igenom listan och om någonting blir success så returnar den success och slutar
/// iterera igenom listan. om någonting är running så skickar den tillbaks running
/// </summary>
public class Selector : Node
{
    protected List<Node> children = new List<Node>();
    public Selector(List<Node> nodes) 
    {
        children = nodes;
    }
    public override NodeStates Evaluate()
    {
        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeStates.fail:
                    continue;
                case NodeStates.success:
                    nodeState = NodeStates.success;
                    return nodeState;
                case NodeStates.running:
                    nodeState = NodeStates.running;
                    return nodeState;
                default:
                    continue;
            }
        }
        nodeState = NodeStates.fail;
        return nodeState;
    }
}
/// <summary>
/// Tar en lista av nodes och itererar igenom dem, om någonting returnar fail så returnar även denna fail och
/// avslutar sin iteration genom listan. Om running så returnar den running.
/// </summary>
public class Sequence : Node
{
    protected List<Node> children = new List<Node>();
    public Sequence(List<Node> nodes)
    {
        children = nodes;
    }
    public override NodeStates Evaluate()
    {
        bool anyChildRunning = false;

        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeStates.fail:
                    nodeState = NodeStates.fail;
                    return nodeState;
                case NodeStates.success:
                    continue;
                case NodeStates.running:
                    anyChildRunning = true;
                    continue;
                default:
                    nodeState = NodeStates.success;
                    return nodeState;
            }
        }
        nodeState = anyChildRunning ? NodeStates.running : NodeStates.success;
        return nodeState;
    }
}
/// <summary>
/// tar en node och byter dens return värde: node.success = fail. node.fail = success. Annars running
/// </summary>
public class Inverter : Node
{
    protected Node child;
    public Inverter(Node node)
    {
        child = node;
    }
    public override NodeStates Evaluate()
    {
        switch (child.Evaluate())
        {
            case NodeStates.fail:
                nodeState = NodeStates.success;
                return nodeState;
            case NodeStates.success:
                nodeState = NodeStates.fail;
                return nodeState;
            case NodeStates.running:
                nodeState = NodeStates.running;
                return nodeState;
        }
        nodeState = NodeStates.success;
        return nodeState;
    }
}
/// <summary>
/// In parametern är ett delegate, alltså typ en metod, den metoden måste ha returnvärdet NodeStates
/// </summary>
public class LeafNode : Node
{
    public delegate NodeStates leafNodeDelegate();
    private leafNodeDelegate runAction;

    /// <summary>
    /// OBS! inparametern ska vara en metod med returnvärdet NodeStates
    /// </summary>
    /// <param name="action"></param>
    public LeafNode(leafNodeDelegate action)
    {
        runAction = action;
    }
    public override NodeStates Evaluate()
    {
        switch (runAction())
        {
            case NodeStates.success:
                nodeState = NodeStates.success;
                return nodeState;
            case NodeStates.fail:
                nodeState = NodeStates.fail;
                return nodeState;
            case NodeStates.running:
                nodeState = NodeStates.running;
                return nodeState;
            default:
                nodeState = NodeStates.fail;
                return nodeState;
        }
    }
}
public class AlwaysFalse : Node
{
    Node child;
    public AlwaysFalse(Node child)
    {
        this.child = child;
    }
    public override NodeStates Evaluate()
    {
        child.Evaluate();
        return NodeStates.fail;
    }
}
    

