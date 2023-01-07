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
7. コマンドライン引数を次のように設定
	-bin [OpenJTalkのパス] -text "$text" -m [htsvoiceファイル] -x [辞書のパス] -ow "$file"
	
	そのほかの引数はお好みで設定してください。OpenJTalkと同じです。

これでAquesTalkやVOICEVOXと同様にOpenJTalkを扱えるようになります。

## 使用要件
本ソフトを使用した動画には、このGitHubのURLを適当に載せておいてくれたら嬉しいです
URL: https://github.com/Nikochan2525/OpenJTalkInterfaceForYMM4
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
### Version 2.0.0.0
> ユーザ辞書を追加しました
### Version 1.2.0.0
> YMMのプロジェクトロード時に正しく音声が生成されない問題を修正
### Version 1.1.0.0
> 引数にスペースがある場合に正常に動作しない問題を修正
### Version 1.0.0.0
> 公開。それだけ