# CyberDisruptor
- 概要  
[2023年の日本ゲーム対象アマチュア部門](https://awards.cesa.or.jp/2023/amateur/)に向けて開発していた3Dアクションゲームです。  
この制作では、私自身が綺麗なソースコードを作りたいと考えるようになったきっかけのプロジェクトで、今回のソースコードでは命名にとても悩んだり、1つのクラスにどれほどの機能を詰め込むべきかを手探りで考えながら実装を行いました。

- 制作期間  
2023年4月~2023年9月

- 使用技術  
[AnimatorEvent](https://qiita.com/aimy-07/items/58e77d3396ded286affc)、[Arbor3](https://arbor.caitsithware.com/)、[Cinemachine](https://unity.com/ja/unity/features/editor/art-and-design/cinemachine)、[DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676?locale=ja-JP)、[RaycastVisualization](https://baba-s.hatenablog.com/entry/2022/06/22/090000)、[UniRx](https://github.com/neuecc/UniRx)  

- 制作ゲームジャンル  
3Dアクションゲーム  

- 制作人数  
4人  

- 役割  
[前回の制作](https://github.com/hamster3156/CyberBlade)ではプログラマー兼プランナーとしてゲーム開発を行っていましたが、プランナーとして企画を考えたり指示を出すよりも、プログラマーとして機能実装したり、チームメンバーが悩んでいる部分を一緒に考えて解決に導いたりなど、プログラマーの方が自身の性格にあっていると考えプログラマーとして参加し、プレイヤー操作の開発を行いました。  

- Arbor3を利用した感想   
今回のプロジェクトでは、Arbor3を利用してゲーム開発を行いました。  
![image](https://github.com/user-attachments/assets/d5fa1398-bd63-4449-9752-059aec72fa45)  
基本的な利点は[AnimatorStateMachine](https://github.com/hamster3156/CyberBlade)と似ている部分が多いですが、オブジェクト生成処理やランダム数値生成など簡単な処理をエディタ上で作成することができたり、ステートクラスにAwakeやFixedUpdateが用意されているなど、AnimatorStateMachineの痒いところに手が届いたステートマシンだと利用して感じました。 

- 今回の制作で工夫した点  
[動画](https://github.com/user-attachments/assets/2ee700ee-54b7-41e2-b3c8-da5d5a827438)のキャラクターや武器の透明・表示処理を作成する上で、最初はMaterialのa値を直接変更していましたが、連続で変更するとMaterialの色がおかしくなる問題がありました。そこで、DOTweenのDOFadeとAnimatorEventを利用して一度だけ透明・表示処理を行うようにすることでMaterialの表示問題を解決することができました。

- Scriptの内容について
各フォルダーに入ってる簡単な内容の記載です。  
 - AfterImageには残像オブジェクトを生成するクラス  
 - Fadeにはオブジェクトを透明・表示にさせるクラス  
 - Mainにはステートで管理していない入力・地面検知クラス   
ParametorにはプレイヤーのHPや移動速度などのパラメーターを調整できるクラス  
Physicにはプレイヤーの当たり判定に関するクラス
ShiftWeponには高速移動時に投げる武器に関するクラス  
StateにはGround,Air,Damageなど分割したArborのステートクラス  

- 実行ファイルは以下の場所にあります  
https://drive.google.com/drive/folders/1eKVdWUGzMPcK_nm64tScOOo2K3SLHSp_?usp=sharing
