# CyberDisruptor
- 概要  
[2023年の日本ゲーム対象アマチュア部門](https://awards.cesa.or.jp/2023/amateur/)に向けて開発していた3Dアクションゲームです。  
今回の開発では、[前作](https://github.com/hamster3156/CyberBlade)の開発経験を元に、操作性を向上させた3Dアクションゲームを作りたいと考え、開発を行いました。開発途中では、企画の方向性に悩んだり、チーム内の意見が合わずに開発を続けることが困難な時期もありました。そこで、チームの方向性を合わせるために、企画について話し合う時間を積極的に設けました。それぞれが考えるゲームの完成像を共有し、企画と擦り合わせることで、方向性を修正することができました。長期間の開発で企画が迷走し、最終的に作品を完成させることはできませんでしたが、チームの方向性を修正したり、長期間にわたってチームメンバーと真剣に開発に取り組めたことから、非常に良い経験を得ることができました。

- 制作期間  
2023年2月~2023年12月

- 制作ゲームジャンル  
3Dアクションゲーム  

- 制作人数  
4人  

- 役割  
今回のプロジェクトでは、プランナー兼プログラマーとしてゲーム開発に関わりました。  
プログラマーとして担当した部分はプレイヤーの操作開発です。

- 使用技術  
[AnimatorEvent](https://qiita.com/aimy-07/items/58e77d3396ded286affc)、[Arbor3](https://arbor.caitsithware.com/)、[Cinemachine](https://unity.com/ja/unity/features/editor/art-and-design/cinemachine)、[DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676?locale=ja-JP)、[RaycastVisualization](https://baba-s.hatenablog.com/entry/2022/06/22/090000)、[UniRx](https://github.com/neuecc/UniRx)  

- Arbor3を利用した感想   
今回のプロジェクトでは、Arbor3を利用してゲーム開発を行いました。  
![image](https://github.com/user-attachments/assets/d5fa1398-bd63-4449-9752-059aec72fa45)  
基本的な利点は[AnimatorStateMachine](https://github.com/hamster3156/CyberBlade)と似ている部分が多いですが、オブジェクト生成処理やランダム数値生成など簡単な処理をエディタ上で作成することができたり、ステートクラスにAwakeやFixedUpdateが用意されているなど、AnimatorStateMachineの痒いところに手が届いたステートマシンだと利用して感じました。 

- 今回の制作で工夫した点  
[動画](https://github.com/user-attachments/assets/2ee700ee-54b7-41e2-b3c8-da5d5a827438)のキャラクターや武器の透明・表示処理を作成する上で、最初はMaterialのa値を直接変更していましたが、連続で変更するとMaterialの色がおかしくなる問題がありました。そこで、DOTweenのDOFadeとAnimatorEventを利用して一度だけ透明・表示処理を行うようにすることでMaterialの表示問題を解決することができました。

- Scriptの内容について
各フォルダーに入ってるクラスの簡単な内容の記載です。

| フォルダー名 | 機能の簡単な紹介 |
|:------------:|--------------------------| 
| AfterImage   | 残像オブジェクトに関連するクラス |             
| Fade         | オブジェクトを透明・表示にさせるクラス |
| Main         | ステートで管理していない入力・地面検知などのクラス |
| Parametor    | HPや移動速度などのパラメーターを調整できるクラス |
| Physic       | 当たり判定に関するクラス |
| ShiftWepon   | 高速移動時に投げる武器に関するクラス |
| State        | Ground,Air,Damageなど分割したArborステートクラス |

- 実行ファイルは以下の場所にあります  
https://drive.google.com/drive/folders/1eKVdWUGzMPcK_nm64tScOOo2K3SLHSp_?usp=sharing
