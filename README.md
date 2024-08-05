# CyberDisruptor
3年生の前期から中期にかけて開発していた3Dアクションゲームです。

- 制作人数  
4人  

- 担当箇所  
プレイヤー操作の開発を行いました。  

- 実行ファイルは以下の場所にあります  
https://drive.google.com/drive/folders/1eKVdWUGzMPcK_nm64tScOOo2K3SLHSp_?usp=sharing

- フォルダーの内容について
AfterImageには残像オブジェクトを生成するクラス  
Fadeにはオブジェクトを透明・表示にさせるクラス  
Mainにはステートで管理していない入力・地面検知クラス   
ParametorにはプレイヤーのHPや移動速度などのパラメーターを調整できるクラス  
Physicにはプレイヤーの当たり判定に関するクラス
ShiftWeponには高速移動時に投げる武器に関するクラス  
StateにはGround,Air,Damageなど分割したArborのステートクラス  

- 今回のスクリプトについて  
Arbor3StateMachineを利用してゲーム開発を行いました。  
![image](https://github.com/user-attachments/assets/d5fa1398-bd63-4449-9752-059aec72fa45)  
基本的な利点は[AnimatorStateMachine](https://github.com/hamster3156/CyberBlade)と同じですが、
オブジェクト生成処理やランダム数値生成など簡単な処理をエディタ上で作成することができたり、ステートクラスに
Awakeの処理やFixedUpdateが用意されているなど、AnimatorStateMachineの痒いところに手が届いていない部分を使いやすくしたステートマシンだと感じました。  

- 今回の制作で工夫した点  
[動画](https://github.com/user-attachments/assets/2ee700ee-54b7-41e2-b3c8-da5d5a827438)のキャラクターや武器を透明にする処理を作成しました。
キャラクターや武器の透明・表示処理を作成する上で、最初はMaterialのa値を直接変更していましたが、連続で変更するとMaterialの色がおかしくなる問題がありました。
そこで、DOTweenのDOFadeとAnimatorEventを利用して一度だけ透明・表示処理を行うようにすることでMaterialの表示問題を解決することができました。
