# 魔王の城

TowerDefenseゲームをTileMapを用いて制作する。
Playerを操作するTowerDefenseゲームです。

## 制作意図や目的
UnityとC#の勉強をし理解を深めつつ、ゲームとして完成を目指す。
得た知識をなるべく盛り込む。

開発環境　Unity 2019.4

使用言語　C#


## ゲーム説明

Playerを移動し、Spaceを押すことでPlayのいる位置に兵器を置くことができます。

兵器を置く、兵器を強化するとコストを使用します。敵を倒すとコストを入手することができます。

20秒間拠点の耐久値が0にならなければWaveClearになり、次のWaveに進みます。

WaveClearごとに敵の生成時間が短くなります。

制限されてるコスト内で敵を倒しWave5まで乗り切るとゲームクリアです。


## 操作説明
Playerの移動　WASD

兵器の設置　Space

設置する兵器を切り替え　画面左にある兵器ボタンを右クリック

設置した兵器の強化　設置した兵器を右クリック
