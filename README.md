# OpenJTalkInterfaceForYMM4
## 概要
Open_JTalkをYMM4で使えるようにするものです

## 導入
1. Open_JTalkをビルド
2. Releaseからzipファイルをダウンロード
3. 適当な場所に解凍(管理者権限を要求しない場所へ)
4. YMM4を開く
5. YMM4の 表示→キャラクター設定→ボイス→声質 を、コマンドライン/日本語 に設定
6. 実行ファイルを展開したフォルダ内のIOpenJTalkforYMM4.exeに指定
7. 	
	1. character.iniを次のように記述します。
```ini
[キャラクター名]
HTSVoice=<HTSVoiceのパス>
SamplingFrequency=<サンプリング周波数>
FramePeriod=<フレームピリオド>
All-passConstant=<オールパス定数>
PostFilteringCoefficient=<ポストフィルタリング係数>
SpeechSpeed=<スピーチ速度係数>
AdditionalHalf-tone=<追加ハーフトーン>
Threshold=<有声音/無声音スレッショルド>
WeightOfGVSpectrum=<スペクトラム向けGVの重さ>
WeightOfGVlogF0=<log F0向けGVの重さ>
Volume=<音量>
```
値は任意に書き換えてください。
2. Config.iniを次のように書き換えます。
```ini
[Config]
JTalkBinaryPath=<OpenJTalkのパス>
DictionaryPath=<辞書ディレクトリのパス>
```

3. コマンドライン引数を次のように設定します。
```cmd
-character [キャラクター名] -text "$text" -ow "$file"
```
これで設定は完了です。

または、

コマンドライン引数を次のように設定
```cmd
-bin [OpenJTalkのパス] -text "$text" -m [htsvoiceファイル] -x [辞書のパス] -ow "$file"
```
そのほかの引数はお好みで設定してください。OpenJTalkと同じです。

これでAquesTalkやVOICEVOXと同様にOpenJTalkを扱えるようになります。

## 使用要件
<br>
本ソフトを使用した動画には、このGitHubのURLを適当に載せておいてくれたら嬉しいです
<br>
URL: https://github.com/Nikochan2525/OpenJTalkInterfaceForYMM4
<br>
Twitterその他で布教してくれたらもう嬉しすぎて飛び上がります  

## 動作確認環境
- フレームワーク: .NET6.0
- OS: Windows10

## その他
Windows11? 動作するんじゃね?知らんけど  
何故C#か? 楽だから(適当)  
バグ等あったらTwitterまたはGitHubのissueにお願いします。  
Twitter: @niko_musubine

## パッチノート
### Version3.0.0.0
> iniファイルでキャラクター設定とバイナリ、辞書の設定ができるようになりました。  
> これにより、YMMのキャラ設定におけるコマンドライン引数を簡略化することができます。
### Version2.1.0.0
> UserDictionaryGUIの挙動を修正  
> Alt+十字上下 で順序を変更可能になりました。
### Version 2.0.0.0
> ユーザ辞書を追加しました
### Version 1.2.0.0
> YMMのプロジェクトロード時に正しく音声が生成されない問題を修正
### Version 1.1.0.0
> 引数にスペースがある場合に正常に動作しない問題を修正
### Version 1.0.0.0
> 公開。それだけ
