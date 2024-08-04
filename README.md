# CyberDisruptor
3年生の前期から中期にかけて開発していたゲームです。

- 制作人数  
4人  

- 担当箇所  
プレイヤー操作の開発を行いました。  

- 実行ファイルは以下の場所にあります  
https://drive.google.com/drive/folders/1MeNa886io-pL4Bw_7xPHlfp2ZuzUhtGS

- 今回のスクリプトについて
ArborStateMachineを利用してゲーム開発を行いました。
![image](https://github.com/user-attachments/assets/d5fa1398-bd63-4449-9752-059aec72fa45)  
今回の制作では、AnimatorStateMachine以外にステートマシン利用したいと考えArborを利用しました。
AnimatorStateMachineよりもスクリプト面がかなり使いやすいと感じていて、特にステートの開始処理としてAwakeが
作成されていて、初期化処理を行いやすいと感じました。

Mainフォルダーにはステートで管理していないクラスが入っています。
StateフォルダーにはArborのクラスが入っていて、Ground,Air,Damageの役割ごとにクラスを分割して管理しています。
