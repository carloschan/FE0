using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardData  {
    public enum CHARACTER_CLASS { 下級職, 上級職 };
    public enum FACTION {
        光の剣,
        聖痕,
        白夜,
        暗夜,
        メダリオン,
    };
    public enum CLASS {
        白の血族,
        ダークプリンス,
        歌姫,
        剣聖,
        侍,
        聖天馬武者,
        天馬武者,
        弓聖,
        弓使い,
        戦巫女,
        巫女,
        上忍,
        忍,
        山伏,
        修験者,
        槍聖,
        槍術師,
        メイド,
        金鵄武者,
        陰陽師,
        呪い師,
        修羅,
        鬼人,
        九尾の狐,
        妖狐,
        ダークプリンセス,
        ダークブラッド,
        パラディン,
        ソシアルナイト,
        レヴナントナイト,
        ドラゴンナイト,
        ダークナイト,
        ダークマージ,
        ストラテジスト,
        ロッドナイト,
        ブレイブヒーロー,
        グレートナイト,
        ドラゴンマスター,
        ボウナイト,
        ソーサラー,
        アドベンチャラー,
        シーフ,
        ジェネラル,
        アーマーナイト,
        アクスファイター,
        バトラー,
        バーサーカー,
        マーナガルム,
        ガルー,
        マーシナリー,
    };
    public enum GENDER {  男, 女 };
    public enum WEAPON {
        剣,
        槍,
        斧,
        弓,
        魔法,
        杖,
        竜石,
        暗器,
        牙
    };
    public enum TYPE {
        アーマー,
        獣馬,
        飛行,
        竜,
        シャープ,
    };
    public enum RANGE { Range1_2, Range1, Range2 };

    public enum RARE { N,SR };




    public string title { get; set; }
    public string name {   get; set;  }
    public string cardNo {  get; set;  }
    public RARE rare { get; set; }
    public int cardId { get; set; }
    public int attendCost { get; set; }
    public int classChangeCost { get; set; }
    public CHARACTER_CLASS characterClass {  get; set;  }
    public CLASS arms { get; set; }
    public int power { get; set; }
    public int support { get; set; }
    public List<TYPE> type { get; set; }
    public WEAPON weapon { get; set; }
    public FACTION faction { get; set; }
    public GENDER gender { get; set; }
    public RANGE range { get; set; }
    public string illustrationPath { get; set; }

    public static int debugIdx = 0;

    public CardData()
    {
        switch (debugIdx++)
        {
            case 0:
                title = "轟雷の剣聖";
                name = "リョウマ";
                cardNo = "B02-006";
                rare = RARE.SR;
                attendCost = 5;
                classChangeCost = 4;
                characterClass = CHARACTER_CLASS.上級職;
                arms = (CLASS)3;
                power = 70;
                support = 10;
                faction = FACTION.白夜;
                gender = GENDER.男;
                weapon = WEAPON.剣;

                type = new List<TYPE>();

                range = RANGE.Range1;


                illustrationPath = "B02-006_SR-thumb-240xauto-1670";
                break;
            case 1:
                title = "風纏う神射手";
                name = "タクミ";
                cardNo = "B02-010";
                rare = RARE.SR;
                attendCost = 4;
                classChangeCost = 3;
                characterClass = CHARACTER_CLASS.上級職;
                arms = CLASS.弓聖;
                power = 60;
                support = 20;
                faction = FACTION.白夜;
                gender = GENDER.男;
                weapon = WEAPON.弓;

                type = new List<TYPE>();
                illustrationPath = "B02-010_SR-thumb-240xauto-1677";
                range = RANGE.Range1;
                break;
            case 2:
                title = "天才肌の少女武者";
                name = "マトイ";
                cardNo = "B02-046";
                rare = RARE.N;
                attendCost = 1;
                classChangeCost = -1;
                characterClass = CHARACTER_CLASS.下級職;
                arms = CLASS.天馬武者;
                power = 30;
                support = 30;
                faction = FACTION.白夜;
                gender = GENDER.女;
                weapon = WEAPON.槍;

                type = new List<TYPE>();

                type.Add(TYPE.飛行);
                type.Add(TYPE.獣馬);
                illustrationPath = "B02-046_N-thumb-240xauto-1730";
                range = RANGE.Range1;
                break;
            case 3:
                title = "天才肌の少女武者";
                name = "マトイ";
                cardNo = "B02-046";
                rare = RARE.N;
                attendCost = 1;
                classChangeCost = -1;
                characterClass = CHARACTER_CLASS.下級職;
                arms = CLASS.天馬武者;
                power = 30;
                support = 30;
                faction = FACTION.白夜;
                gender = GENDER.女;
                weapon = WEAPON.槍;

                type = new List<TYPE>();

                type.Add(TYPE.飛行);
                type.Add(TYPE.獣馬);
                illustrationPath = "B02-046_N-thumb-240xauto-1730";
                range = RANGE.Range1;
                break;
            case 4:
                title = "天才肌の少女武者";
                name = "マトイ";
                cardNo = "B02-046";
                rare = RARE.N;
                attendCost = 1;
                classChangeCost = -1;
                characterClass = CHARACTER_CLASS.下級職;
                arms = CLASS.天馬武者;
                power = 30;
                support = 30;
                faction = FACTION.白夜;
                gender = GENDER.女;
                weapon = WEAPON.槍;

                type = new List<TYPE>();

                type.Add(TYPE.飛行);
                type.Add(TYPE.獣馬);
                illustrationPath = "B02-046_N-thumb-240xauto-1730";
                range = RANGE.Range1;
                break;
            default:
                title = "天才肌の少女武者";
                name = "マトイ";
                cardNo = "B02-046";
                rare = RARE.N;
                attendCost = 1;
                classChangeCost = -1;
                characterClass = CHARACTER_CLASS.下級職;
                arms = CLASS.天馬武者;
                power = 30;
                support = 30;
                faction = FACTION.白夜;
                gender = GENDER.女;
                weapon = WEAPON.槍;

                type = new List<TYPE>();

                type.Add(TYPE.飛行);
                type.Add(TYPE.獣馬);
                illustrationPath = "B02-046_N-thumb-240xauto-1730";
                range = RANGE.Range1;
                break;
        }

    }



}
