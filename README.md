# 概要
2023年6月～2023年12月の時期に開発していたゲームで、自身が作成したスクリプトをまとめたリポジトリです。

# 実行ファイルは以下の場所にあります
https://drive.google.com/drive/folders/1eKVdWUGzMPcK_nm64tScOOo2K3SLHSp_?usp=drive_link  

# フォルダーの内容について

| フォルダー名 | 機能の簡単な紹介 |
|:------------:|--------------------------| 
| AfterImage   | 残像オブジェクトに関連するクラス |             
| Fade         | オブジェクトを透明・表示にさせるクラス |
| Main         | ステートで管理していない入力・地面検知などのクラス |
| Parametor    | HPや移動速度などのパラメーターを調整できるクラス |
| Physic       | 当たり判定に関するクラス |
| ShiftWepon   | 高速移動時に投げる武器に関するクラス |
| State        | Ground,Air,Damageなど分割したArborステートクラス |

# Arbor3を利用した感想   
今回のプロジェクトでは、Arbor3を利用してゲーム開発を行いました。  
![image](https://github.com/user-attachments/assets/d5fa1398-bd63-4449-9752-059aec72fa45)  
基本的な利点は[AnimatorStateMachine](https://github.com/hamster3156/CyberBlade)と似ている部分が多いですが、オブジェクト生成処理やランダム数値生成など簡単な処理をエディタ上で作成することができたり、ステートクラスにAwakeやFixedUpdateが用意されているなど、AnimatorStateMachineの痒いところに手が届いたステートマシンだと利用して感じました。 

# 今回の制作で工夫した点  
[動画](https://github.com/user-attachments/assets/2ee700ee-54b7-41e2-b3c8-da5d5a827438)のキャラクターや武器の透明・表示処理を作成する上で、最初はMaterialのa値を直接変更していましたが、連続で変更するとMaterialの色がおかしくなる問題がありました。そこで、DOTweenのDOFadeとAnimationEventを利用して一度だけ透明・表示処理を行うようにすることでMaterialの表示問題を解決することができました。

# 実行ファイル  
https://drive.google.com/drive/folders/1eKVdWUGzMPcK_nm64tScOOo2K3SLHSp_?usp=sharing
