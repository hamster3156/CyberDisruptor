# CyberDisruptor
3年生の前期から中期にかけて開発していたゲームです。

- 制作人数  
4人  

- 担当箇所  
プレイヤー操作の開発を行いました。  

- 実行ファイルは以下の場所にあります  
https://drive.google.com/drive/folders/1MeNa886io-pL4Bw_7xPHlfp2ZuzUhtGS

- 今回のスクリプトについて  
Arbor3StateMachineを利用してゲーム開発を行いました。  
![image](https://github.com/user-attachments/assets/d5fa1398-bd63-4449-9752-059aec72fa45)
基本的な利点は[AnimatorStateMachine](https://github.com/hamster3156/CyberBlade)と同じですが、
オブジェクト生成処理やランダム数値生成など簡単な処理をエディタ上で作成することができたり、ステートクラスに
Awakeの処理やFixedUpdateが用意されているなど、AnimatorStateMachineの痒いところに手が届いていない部分を使いやすくしたステートマシンだと感じました。  
Mainフォルダーにはステートで管理していない入力・地面検知クラスが入っています。  
StateフォルダーにはArborのステートクラスが入っていて、Ground,Air,Damageの役割ごとにクラスを分割して管理しています。
