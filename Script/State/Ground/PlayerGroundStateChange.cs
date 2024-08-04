using UnityEngine;
using Arbor;

[AddComponentMenu("")]
public class PlayerGroundStateChange : StateBehaviour 
{
	// 地面検知変数
    [SlotType(typeof(PlayerGroundCheck))]
    private FlexibleComponent groundCheck = new FlexibleComponent(FlexibleHierarchyType.Self);

	// 落下ステートに遷移するリンク
	public StateLink Fall;

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
		if(ground.IsGround == false) Transition(Fall);
    }
}
