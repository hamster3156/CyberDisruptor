using UnityEngine;
using Arbor;

[AddComponentMenu("")]
public class PlayerAirStateChange : StateBehaviour 
{
	// 地面検知変数
    [SlotType(typeof(PlayerGroundCheck))]
    private FlexibleComponent groundCheck = new FlexibleComponent(FlexibleHierarchyType.Self);

	// 状態遷移のリンク
	public StateLink Movement;

	// Use this for awake state
	public override void OnStateAwake() 
	{
        // 地面検知の取得
        groundCheck = (FlexibleComponent)GameObject.Find("Player").GetComponent<PlayerGroundCheck>();
	}
	
	// OnStateUpdate is called once per frame
	public override void OnStateUpdate() 
	{
		// GroundCheckの変数を呼び出す
        var ground = groundCheck.value as PlayerGroundCheck;

		// プレイヤーが地面にいるかどうかでステートを遷移
		if(ground.IsGround == true) Transition(Movement);
    }
}
