using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class IsNavigationTypeOf
{
    // private NavigationActivity m_ActivityToCheckFor;
    // private NPC Npc;
    // public IsNavigationTypeOf(NavigationActivity activity, NPC npc) : 
    //     base($"Is Navigation Activity {activity}?")
    // {
    //     m_ActivityToCheckFor = activity;
    //     Npc = npc;
    // }

    // protected override void OnReset() { }

    // protected override NodeStatus OnRun()
    // {      
    //     if (GameManager.manager == null || Npc == null)
    //     {
    //         StatusReason = "GameManager and/or NPC is null";
    //         return NodeStatus.Failure;
    //     }

    //     StatusReason = $"NPC Activity is {m_ActivityToCheckFor}";

    //     return Npc.navigationActivity == m_ActivityToCheckFor ? NodeStatus.Success : NodeStatus.Failure; 
    // }
}
