﻿using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day2
    {

        public int Compute1()
        {
            var count = 0;
            var regex = new Regex(@"(\d+)-(\d+) (\w): (\w+)");
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(input));
            var reader = new StreamReader(stream);

            while (true)
            {


                var line = reader.ReadLine();
                if (string.IsNullOrEmpty(line)) return count;

                var match = regex.Match(line);
                if (!match.Success) continue;

                var min = int.Parse(match.Groups[1].Value);
                var max = int.Parse(match.Groups[2].Value);
                var chr = match.Groups[3].Value[0];
                var str = match.Groups[4].Value;

                var chcount = str.Split(chr).Length - 1;
                if (chcount >= min && chcount <= max) count += 1;
            }
        }

        public int Compute2()
        {
            var count = 0;
            var regex = new Regex(@"(\d+)-(\d+) (\w): (\w+)");
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(input));
            var reader = new StreamReader(stream);

            while (true)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrEmpty(line)) return count;

                var match = regex.Match(line);
                if (!match.Success) continue;

                var min = int.Parse(match.Groups[1].Value);
                var max = int.Parse(match.Groups[2].Value);
                var chr = match.Groups[3].Value[0];
                var str = match.Groups[4].Value;

                if (str[min - 1].Equals(chr) ^ str[max - 1].Equals(chr)) count += 1;
            }
        }



        private static string input =
@"4-5 m: mmpth
1-7 r: rszchrrrzgr
2-5 w: dgqtwwkwwc
10-11 w: ldslpwbbqwpwtd
3-4 s: sszss
1-6 l: llmjxlbt
8-11 c: ccctccccccw
1-11 l: tllllllplllzjllgz
5-8 j: ljjjjjjqcw
11-12 s: sssssqsslsvhfs
7-10 f: fffffmfqfffffff
3-8 m: mmmsqmmmh
3-9 w: dwzvsttjw
7-8 l: llllflhg
10-13 f: jfkwhzrtktphc
5-6 p: pvldppqvcd
9-17 s: sssssssstssssssscsss
4-7 k: zlwhcwkrckwkskxkgh
12-14 n: nnnnnnwncnnjnn
6-8 g: gkwzbwhgmgrqqlksswg
7-8 d: drpbhdvwddd
6-8 d: dlmphvwbnnddrd
5-6 m: mmzmmmpm
6-13 x: xdcxhkqqxznxwhtxdxt
4-14 c: pncwncdcccccdmwccccc
14-15 s: gsrsssssbsssstkssmb
1-3 m: dhmzmlnfmsm
8-12 l: llmlrllchlwhlktpxdf
3-5 g: njgkglgx
11-14 v: vvvvmwvkvtqvgxvvvlvv
1-4 h: hhhq
1-2 l: dfrmsp
4-5 n: nkknnkrflkpnnn
10-12 w: dwwwwwwlwwww
13-14 h: xnhhhrvwpnkmhx
11-12 l: lllllklrllrx
12-14 d: ldrvtddrddvdqgj
10-11 p: bxpqplqtqzhjv
2-5 t: bmltz
8-10 j: ljjjjjjmjjj
5-6 x: xxxvll
5-7 b: bxbbbbbk
5-11 t: ttjmsttwtdh
2-3 p: dmxxrpnppd
1-4 x: xxxxxgx
3-6 v: vwcvvv
4-5 l: lqpll
14-20 p: psxqnpfpnmpsppppppsp
16-18 g: lgggkgggggggggggglg
3-5 b: tbfbbg
6-8 n: nsnnnnnnnw
2-4 p: dppndlmtnt
11-15 q: tqqqqqqqqqqqqqq
13-14 r: rrrrrrrrrrrrbx
3-6 s: shsssfc
7-9 l: mlghdlhwfbcqmv
1-2 k: mlwk
13-17 v: hbvlqvknvvsvzvvvvv
3-4 g: bmngrgttz
9-10 h: hhmhhhhhtc
1-15 m: nmmmmmmmmmmmmmmm
12-13 k: kkkkkkkkkjnfckk
1-8 k: skkkzkkkk
5-7 j: jjjjjjhj
12-14 g: gmgxwglgggggbggqgg
9-11 p: pppxppqpprlgpp
14-15 j: cpjjjjjjjjjjjjjj
4-12 l: lnblzbdmpwwrbmnr
3-8 d: dddhdmcrddnddnddjl
5-9 g: kghqfsggf
18-20 g: gggggggggggggggggngw
3-5 q: rkqlqfzvwqqqksntqdz
8-16 m: dkmmzgmdmmmhwmxmxm
7-9 q: qnqhtkqqqqqqjvcs
1-3 m: mmlm
1-10 b: bjfkmblmbbtvxzp
4-5 c: ccldccsq
4-7 k: dzbkksrkkkbkwd
1-7 r: rrrrrrp
9-10 g: qgggtgnlszgggs
6-7 g: ggsgjfq
10-11 n: nnnnnnnngnnn
3-4 c: ftchcgcvlgzckdrg
13-15 p: pppnppppppppzhpplhp
2-7 k: xnlhlxsdzkvhw
5-8 m: mmmbmqmnfmtm
7-8 d: sdddddcjd
3-4 b: fnjr
8-14 c: cccccrjcccccccchcfqc
6-11 m: mmmmmmmmmmmmmmmmmmmm
5-7 l: lllllwhtl
5-7 z: zzbzrzg
6-10 n: ncnznxzcwvcn
2-4 f: xpkffxmlwtgzjppcdf
19-20 c: cccccccccccccxcccclf
4-14 w: wwlmwwfpwwzjdww
11-17 f: fvfffvfffffxffffff
12-13 s: gtshbpzgxnsss
5-6 m: mmsjjgmd
15-17 x: xxxxxxxwxxxxxxhxx
5-9 f: kgfjmbctt
6-7 k: wkknmkk
2-4 c: cccc
5-9 s: grsgskjnsmsg
3-5 q: qqqqqps
9-11 f: fffbfbfffffjfffcffkg
7-9 q: qqqqknqqcmqqqq
17-18 x: xxxxxxxxxxxxxxxxxq
2-10 j: cmhglhqjdg
10-11 h: pnhzhkgxbhkdm
8-12 j: qjjjjwjjjlzjbjjdcjj
15-16 q: qqqqqqqqqqkckvrq
6-15 f: fkfffffffsfcknf
4-17 c: vgpsglczjdfcnqtbnl
10-11 k: mmxktwkkwbvfkkdkkknj
11-12 s: ssssssspsssk
3-5 l: lcqdh
2-3 m: mqqq
6-7 k: fkkkjkv
13-15 g: gqlgdsgggggxggg
7-9 r: jvnkhhrrrvrsfxkrtd
14-15 f: ffrffffffffqpkllfxff
3-4 d: gjvf
9-12 x: hxxxrxqwxxjxxq
5-7 k: wdkkkdfttqkdkng
6-7 x: xxxvxhx
6-13 c: ccsclccccfmcdb
5-17 c: cdccsvcccccccccck
14-15 s: sssssssssksssnx
6-7 j: jjjwhqt
14-15 s: kssssssssssspcss
3-6 w: tnmlbhw
1-4 g: lqgrtcgn
1-11 b: drrwhbwzqdj
7-8 r: vrnvdtrr
4-8 w: vwwwwwwwww
6-7 v: vvvvvvn
7-14 t: sxtxhdxcwqdgrtqs
2-7 w: ptgsdwghvjwmwbgrqqs
1-2 l: llllgll
3-4 n: npnnbg
3-5 h: mxhhhwhf
10-15 j: bjjjtnjppjjjzcjjsw
8-11 r: srkkhrrcwrbrptr
2-4 t: qttwv
8-9 t: tfhtldttt
5-15 r: lwdmlgrrrrtrrrpxgrp
8-9 d: ddcddddgwd
3-4 b: cbbb
11-12 m: vmmnxtdxmmnm
2-5 r: rwrvc
1-10 s: sssxsfsssssws
1-4 v: kvlxvvv
5-17 h: dhqhhhlxhhhhqhhbhhnc
7-9 d: bdddpkdnfrnmb
3-6 x: qvstbxvkqfnxwnk
16-18 m: mlmpnmmstmmlmcmbmh
8-9 h: hhhhhhvhhhhhhhx
7-10 t: tqcttwwttg
1-9 m: gmvlmmmqmtpfm
3-4 p: ppppp
7-17 d: xddgqhdddcptxdhxc
1-11 f: ffffffffffff
13-14 r: dmsrnztgrrlvgt
10-11 r: rrrrrrrrrrvr
4-5 l: llllll
4-9 k: kkkkbkkkd
3-5 b: vsbbbtrnqzbbd
2-12 j: sjgjnjmhjgcjjcnjjj
5-15 h: lqhvhhtxkhchmhh
7-14 l: sllsfllllllllllllll
12-14 v: pvvvvvvvvvvvvv
4-5 j: jjgjjjg
11-13 q: qqqqmqqqqqdqxq
7-9 p: ppppppphpnp
5-6 s: ssrssz
8-14 j: jjjjjjjjjjjjjjj
6-8 j: jtxjdjmjxjjjrjq
14-16 d: ddddddddkdddddjd
4-6 h: hhhhwhxh
5-12 h: hhphwjhjhhhhhnhhbh
5-9 m: mmmjxhpkmmhmttjdmk
5-9 c: cxcvgtccttwn
6-11 r: bcrrwznbgtrrrr
3-4 t: fpttlttxjwcqmtbw
2-4 b: dmxkbrgpjqtlnq
2-7 r: rdrfprqprkgwpxrrw
10-13 c: ccckpcsccwccccc
5-7 l: vlllllkl
4-11 x: xxxjxxxxvxtx
1-5 h: hhhhh
7-12 s: sjssssxnssmsj
4-5 w: wthmw
6-7 f: fkppffq
7-8 c: cgktccccckcccc
1-2 q: lhnsqrk
2-6 m: dvmmmm
3-6 x: dbxxlxkf
7-14 d: ddsdjslpkfddxjjjkdt
14-16 c: ccqcvctjcccrccgc
1-3 d: dddrkddddkmxr
7-10 w: gpwgskmxsjv
4-5 v: bxhgm
6-7 w: whwhfqw
1-4 x: sxxxrx
2-4 g: ggdggg
15-16 h: hhhbhhhhhhhhhhhh
8-9 s: ssssxsksshss
4-6 w: wwwlwqw
6-8 d: sdpqddddmddw
14-15 j: jjjjjjjnjjjmnjj
2-9 p: pvzstcclp
18-19 g: fgggggggglpgggggztg
15-17 z: zzzznzzzzfzzvztzhzzz
10-11 j: pjjkjkjjjgj
7-12 w: dwbwwbzwwwbvwmwc
3-11 g: vgggwqnwspgg
8-13 w: wwwwwwczwnwtww
1-11 m: mjdtsvwmcqmvd
2-3 c: vcccc
6-17 k: wskkkkkkkkgjkzkkkgmk
8-12 g: mghgrngjggvjgg
4-7 p: pppsppp
6-9 v: vvvvvrvvv
5-14 j: jjmqlzjbjjjlmjjjjj
1-13 f: fkbnffffffffpfff
3-9 r: vrfrxtvwh
2-4 k: fkskpjkkbh
15-19 d: dddddddddddddddddhdd
1-16 s: swtmgswssfwsxpsssss
12-13 s: ssssssssssslssss
1-2 q: wvzvbb
9-14 d: pddddqgcdddjddx
4-6 x: vbqpxxbkxmx
1-5 q: qzqqqqqwfmtqfq
7-17 l: mdlcllllhrlllhpllcl
9-10 q: qqqqqstcgcxqqq
6-11 x: txtxhxxmxhxxx
7-8 c: ccccccppc
12-13 v: vmvmvvvvvvvvxvl
10-12 r: rrrrrzrrglrrrrkr
2-4 s: ssbsv
15-17 g: ggggggggggggggggf
2-7 g: sgvgtgghmtxd
16-17 s: ssssssssssssssvzsss
4-6 n: nnnnqnn
10-15 t: mvtttvtftqzmtjt
10-11 q: qqqqbqqwqrzqqp
6-14 h: hjhhhchhhhjhdd
11-15 b: zbwhdjksbqbdbmb
5-12 r: mrnggrwzrhrrrr
9-16 m: jmmmmmmmmqwmmcxmfmm
7-8 h: qsmhhhtsbhhhjhwl
2-3 h: hkghhm
1-4 k: nkkkk
3-4 z: zzzz
11-13 j: jrjjgbgjxrqjrmjjgckj
3-7 w: wwwqqlwdb
6-9 b: fsbbbqbbqmbb
13-16 n: nnnnnnnnnnnnnnnn
5-7 b: cbbqmbbrhbbz
2-3 l: xjzkbwgfwwcwll
13-14 z: zzzzzzzzzzzzzzzz
6-7 c: ccccccc
4-10 l: lhlslmlwmwbllv
4-5 h: rqhhhhh
3-4 t: dmtttmxhtqz
6-17 n: nnxcnncnnvgcnnrnrmn
3-4 x: xxmxxx
8-10 j: jjjjjjtjtvjj
5-11 d: ldldbnndddddsgdpgj
1-8 n: nsjvhncn
3-8 f: fffffssffxfffhl
1-3 q: kwnq
3-5 k: krkcklkhkspqmqm
3-4 k: dklm
11-14 j: jjjjjjjjjjjjjjj
16-17 v: vvvvvvfvvvvvvvvmx
5-6 m: mmmmmm
10-11 l: wdlgllqlslf
5-6 t: prbwttmtpvwr
6-20 c: cwcctqccthccrpccccct
5-12 s: sssssbssssszs
10-11 k: mkkjklhkktkkkdvkqqkm
11-12 n: nnnnrnnsngnn
2-5 p: ppppl
7-11 f: mvmdfmdwfffffwfrjfqk
13-16 r: rzsrdsrrrrrrprpjrb
13-14 c: ccccccdccccctc
13-18 n: nnnnnnnnnnnncnnnnnn
4-10 r: rvrrdhflcrtwzz
2-9 p: prpczpbpt
3-8 q: qqqqcksqqtx
4-5 d: xxdpmdbvddn
3-4 j: jjlj
2-3 n: nrnnnsnn
3-4 w: pnwkwdn
1-5 w: wlwgw
10-14 x: xxxxxxxxxxxxxxx
5-15 k: kkkkfskkhkkkqpf
3-4 b: bbbbf
3-4 r: jrrr
3-6 h: htkhhhhh
15-16 g: gggggggggggggvgt
17-18 s: ssssssssssgssssssz
14-15 m: pkngmmpjmxmmmms
4-10 f: rsmxvsghdlfff
16-17 z: zvzhzzczdvzkzzghg
16-18 v: vvvvvvvvvvvvvjvvvv
6-9 t: tttttmttttt
5-6 j: hjwxgbjf
3-6 k: kkkkkh
9-13 d: ddpgdfddddddd
14-15 x: xxxxxxxxxxxxxxx
1-4 v: vqvqkrjx
4-5 h: hjhhnh
2-3 v: jvvsrgsvfqlv
9-10 m: mmmmmmmmmm
2-5 f: ffnfpff
4-6 b: bbbbbg
8-9 p: ppjwqpjbnpppwp
5-6 l: lldlllll
9-10 g: gvgqggtgggggg
6-9 x: jxxxxxwxqqvxx
1-4 w: lwwgbt
16-17 d: jcdmgkmfvvlqphbddvhc
1-12 s: sssslssssssnsss
4-5 p: cjnppjllxbrp
13-14 n: jmzjhttnkkgnnn
11-12 t: vrgtrgjrdmtcfzx
16-17 l: lllllnmlvldlllrwmlc
13-16 m: mbdmmmmmmhmsmmmmtb
3-6 z: zszzzzz
1-6 h: hhhqhrbh
3-5 n: rlnnnn
17-19 x: xxxcxxxvxxxxxxxxxxkx
3-10 r: rccrrrhrrx
4-7 h: hhhdhhh
3-6 g: hgfzpmpgg
1-2 x: xxqhhx
5-6 m: jmmmmzf
1-2 j: bjjj
2-3 q: gqst
3-4 g: ggggmbghk
3-4 f: fnfdf
5-6 q: qpqtdg
2-3 t: ptctz
5-6 d: pddvdddsd
1-4 f: ffftff
3-5 b: bbdbg
13-17 f: fgfckcdfffftffxfffff
3-4 w: wwkxsw
2-12 l: hvgqmbqlrnzls
1-2 f: szff
6-7 q: sqqqqnqq
1-4 p: ppvd
3-7 c: cwscbkgc
4-12 s: sssfsssspsfsgss
4-10 t: jstttgststtghzp
5-6 j: jjjjtj
5-9 k: ffkdktkkhbbwkkj
9-15 k: kkkkkzkkkcbkkkkkkkkk
3-17 w: wwgwnnkswwpqwkwdhw
15-20 l: wlllllslllllrllllllk
4-5 r: rrrfm
2-6 l: flllnll
12-13 s: ssssssssssscwn
9-10 r: zpqrrrrrfn
10-13 h: xhhfhzchhjhhtdrhhzh
6-7 b: bbbbnrbb
2-12 f: tffjcjtlnvgf
2-4 g: gtgkg
11-14 n: nnnnnnbrnnnpnnn
15-17 j: jjjjjjjjjjjdjjvjv
10-13 r: brgrrrfrmfrrxrgrrrs
3-13 v: vwvjsftzkvcxvdbxfs
17-19 f: fffffffffmffffffffff
1-4 g: vggxg
5-16 m: csckdfxglfpzmrhcmbnn
4-15 z: gzqjzczzzzjzzhjbz
13-14 w: wwwlwwwwwwwwht
11-13 j: cjjdjjjjsjjjkjjjh
4-15 w: wwwqwwzwwmwwtww
1-2 p: ppjcp
6-10 f: ffjffrfffsgm
17-19 q: qqqqqqqqqqqqqqqqqqqq
3-4 g: gmgz
10-14 s: fpsssdmssdsgsgvss
9-10 m: mmmmmmmmmmmm
1-5 c: fcgns
5-9 t: ttcgtttrtttttq
4-5 v: vvrvvv
2-4 w: xwpw
5-12 d: trdhrqlqwrlsrdfh
3-8 n: bbnnnzrnnjdnb
8-10 w: wwwrwdwjgz
5-6 d: ddddjr
1-3 p: ppqppp
10-19 f: dfbjgffmpvnkhfwjfff
3-6 k: kkkkkkkp
5-7 t: vqtxtxtvk
14-15 c: cccclccccccctcctwc
2-13 x: tmxxzxklxgzxxp
16-17 b: bbbrbbbbbbbbbbbbbb
6-11 h: gmzhhhhmrhqhhm
7-9 k: kkkkkkqkck
4-7 v: pgvdvvx
12-19 f: fffffffffffxffffffff
2-5 p: mphtpp
6-13 t: ctttvwtntqttttq
3-11 t: xntrzxtlnwt
17-18 l: llllllllllllllllll
2-5 c: zczpcfdzblnkcvj
1-5 z: fzzzmz
6-11 f: tdcffpffwqlfffg
16-17 h: hhhhchhhhshhhhhdhh
13-14 m: msmmmkmmmjmmmmzmmnm
6-13 s: nhbscsgcssxsrsssx
5-17 q: qqvqqqhqrkqqqqgqqq
2-4 t: vqtbt
2-5 t: wfdrsrt
6-7 f: ffffzdx
3-13 r: pmrtkvdcrxrnrzw
14-18 r: wrrqrrcrrlrjmrrkrt
15-17 j: jjjjjjjcjjjjjjxjt
8-9 h: zmhhlmhhhdhhhhg
6-15 l: lllllnlfllllllll
3-5 n: nnfnb
16-17 s: sssssssslssssssss
8-9 k: kdkjkkkskkk
2-5 z: gzzmzhdplnwwvlsjnzv
4-15 r: rvmrbsrtrfdqrrb
6-7 l: llllllc
4-7 g: dnggxhgggg
8-9 f: fftffkdff
6-9 r: rrrvrprbrz
2-6 q: qwqqqqq
7-9 h: mzhpwnkhh
3-4 m: mmlh
1-15 v: vjpkvdzvzdlklpn
1-3 r: rrggvr
8-13 d: dwrpdsdjhlddqddhdwdp
3-4 r: fvrrsprrmrrrjgr
4-5 s: zsrssvss
6-7 d: dfdwtcvgvcdfdqdwd
6-12 n: rhnjnndnfnwql
3-5 l: hvlxlcnclqllrw
10-14 l: llllllllljlllplll
6-10 b: bbbbbbbbbbbbbbbbb
8-11 q: qqqqqqqtqqqq
7-8 r: rrrrrrxf
1-4 k: jkkkzsmbtng
13-17 r: rrrrrrlrprrdrrrrl
1-7 t: dfgnvtl
2-6 v: qvvdsm
8-9 l: lltlrlljb
3-4 m: mdmm
17-18 t: ttttttttttttjtxttwtt
9-11 b: bbbbbbbbbbpb
2-3 b: bwnbr
4-10 t: tfdtltstdft
8-10 z: zzfzzzztzzz
6-8 q: qqqqqqqmqnqqq
7-11 r: rzrrrrzrrrr
11-13 k: kkkklklbkmklk
17-18 l: slllllblllllllllclll
10-14 s: ssssssssswssssssss
3-4 s: hwssssgjsp
7-10 g: fbggxgtbngg
6-7 z: tzzzhzjz
3-7 g: ggggggcrg
16-20 g: grgghjcnlmkszxrgtgvv
1-16 n: prqnwnngbgtjsvsj
13-14 z: zszlgqdgtqbzzlpkzz
4-8 f: ffftffnnfp
15-18 s: jrdgssszdxkzhlsfzs
7-8 l: llllllbplllllll
7-10 l: llllbllllll
5-9 b: bbbbwbbblb
4-5 g: gdwhdg
2-3 v: jnfvvv
11-13 h: hhhhhxhhhmzhbj
11-13 r: wbrrrrxgwlvwd
10-12 m: gxmpmmmgmmmmm
11-12 h: hhhhzhhhhjhnhhh
2-13 g: kzkztgnlbqhlgx
4-7 z: zktzzzzx
1-7 q: pqrxqqhfpglwqts
6-7 f: pffvfbtf
6-7 l: tlljjlllnll
4-17 f: flffpffffdfqffscff
2-4 q: bqxqrqrrq
6-8 l: lllnllfll
9-12 r: xrgrrjrrrkgrrqrnr
14-19 r: rrrrrrrrrrrrrrrrrrrr
18-19 w: lbqrkdwwwqgrklcwxwwh
9-10 n: nnnnnnnnnn
7-13 b: bbbbbbbbbbbbb
5-7 k: hkbfskqkjktwkk
9-10 c: tlvmtfcpccwwz
3-7 r: rzzrspmp
7-11 g: gghzgglgggt
4-5 p: pbpwp
3-6 q: qqrqqv
3-13 m: mmmwxmmlmnvmmmmmm
2-6 p: ppplqpfpp
11-15 t: ttttttctttltctgt
2-7 m: cmnhjqm
1-6 m: mzjmtmmmmmdw
1-10 j: jzgjcjjjjspjjjjjgjmj
4-7 r: vmxtrrnrxnnq
8-16 h: hhhhhqhfhhhhhhhhhh
4-10 z: kcmkzmzpzts
14-15 j: jjjjjjjjjzjjjjj
6-11 x: xxkmxxcxxvx
15-16 v: vvvvvvvvvvvvvvgt
6-7 w: wwwwwgqz
5-7 d: dkdpmcddjdd
6-12 s: sssssssssssvs
8-10 q: fcpwtgqqqz
4-5 w: wwwwgwww
8-11 q: qqqqqqqnqqbqqr
3-4 d: dddwddd
9-12 x: qxcgxxxxvdxdkzxxxxx
3-8 k: kqcsjxszvscfvm
4-7 d: mgdddddhkdqb
3-7 x: xjxxljlsfrfnhsxxlv
4-9 g: xgsxgtgggxmgg
1-4 p: tppknptkp
3-7 z: gzzbzzz
1-5 p: ppgpz
15-16 f: fzfffffffffffffwff
5-11 t: tzttvqltfxtg
6-7 v: vhqvzpvv
2-6 f: fmffftfff
9-13 d: dxrdgddddrddddqpgdd
7-8 r: rrrrrnlnrr
5-6 t: btttwm
13-14 g: gggggggggghgblgg
12-19 r: qkgrvfnsrwzrqsrhrrs
15-17 j: jjjjjjjjjjnjjjhjd
4-9 t: btmrbcgttjht
16-17 n: nnnnnnnnvnnnnnnznn
3-7 l: lltlllglll
12-14 v: vvvvvvvvvvvvvv
1-3 q: qqqq
12-14 f: flbfphvwfdffjf
10-11 z: lszzzzzzzzzzz
3-9 k: lvzkkhkkp
4-6 j: fmrjccsjrzjqkfvn
5-11 x: xxxxvmxxxxcxx
1-9 c: wcccccccjxcvccccc
1-4 l: sblllll
1-2 x: xnvlnxs
4-5 f: nffccfff
8-9 p: vpppppprz
1-6 d: vfcqmddw
6-7 b: bbbbbbb
7-13 n: nnnnnnnnnnnnwg
3-6 q: zqqqqqqqqqpqqqq
1-9 g: gbggggdgglgmg
9-13 b: wbbbxnbbqbbbbkbb
4-5 g: gggwr
3-9 r: vnqvvhprzmbhrcb
10-11 m: mmmmmmmmmpm
4-7 c: cgrbccdbcz
5-8 r: rbvrrrrr
7-11 d: ddddjdmddfhvdn
1-2 k: zjkk
2-11 w: wtwwwwwwwwsw
7-8 b: bkbbbbfbbxd
2-4 w: wwnjswwmgbwrwwm
9-18 j: jjjjjjjjkjjjjjjjjs
6-7 t: tttttvwtttt
10-11 v: wvvvbvvvvvvvm
17-20 d: ddddddddddddhddddddn
2-12 q: qdqqqqqqqqqxqqqqqq
5-7 w: rjdswwwwwwqmmpww
1-9 b: bbbbbbbbxcbbbb
4-5 q: qqqwxp
10-15 n: nnmnnnnnncnnnnvn
10-17 f: fnfffffffffffffcff
2-3 p: krpx
8-10 c: cvsccclwccrcjjclc
5-9 t: fpnhtmtqtvwcss
10-13 r: rrxrrrrrrfrrrr
6-9 l: lllllwllt
11-15 n: dnnnnnnhsxgnnhsn
14-16 b: bbbbbbcbbbnbwkbsbm
3-8 g: qhlxgggqvg
11-14 x: ptqcgxnnhpxbgxlm
17-19 g: gggggggcgggbggggngg
10-19 q: qtjrqqwjvqqqknqbqqq
10-13 j: jjjjjjnjjljjsj
1-3 g: mggg
3-7 w: pwtwjdq
1-2 j: jjjp
9-13 z: pszzzglzhzzzzhz
1-2 j: fhhjxz
9-12 l: llllllllsltll
7-9 c: cxdvzcclccz
13-15 g: ggggggggfggggkgg
15-16 z: zjjbgzlxqzzsxzdxz
2-3 z: kxcszzzsrsccg
7-9 t: sttwxgtwlbzbsffttjb
1-6 r: rnmrrrtrrl
4-7 m: pmmtmmq
5-7 f: lggcpdpf
16-19 j: jxjjxjfssrvlmjjfjjmq
2-8 w: wrwswcwvw
4-15 k: kkkpkkkkkkkkkkq
12-15 s: xbssnshpsssgsbk
7-15 z: vzrzzczbhzzzzzzzhz
10-14 w: wwwwwwwwwtwwwwwwp
11-12 k: kkkkkkkkkkkkkckkkkq
15-16 s: sfssvsssssssmspl
4-5 d: dddqd
5-7 h: phsblhnfhchb
8-11 x: rxxxbhxxxjxxxtgsc
11-13 x: xxxxxxqxxxnxvx
5-10 p: jbpzfmphpwpz
9-13 v: vvvvshvvgvsfqvvvv
4-7 r: rrzwrzdr
11-13 f: fffffxffffzfwf
13-14 s: ssssssssssssss
2-11 c: czccdmkhhcvcc
7-11 w: wwsxwwwwwwwwj
7-9 t: ttknmtxvttttnt
1-6 r: rrpjrrrr
5-9 v: vkvvjqvvbvtlv
1-5 q: mwvhpqqqcj
1-5 f: fffff
4-5 r: trrcv
10-12 s: sssssssssssvss
4-5 d: drddzd
4-5 n: nnknl
10-12 x: xxxxfxxrzbxzvtxlxt
4-10 v: sbgsvssbnhmh
3-11 n: dnngqwnnjtnjjg
1-2 w: xdsvzdmzswwwwwwmgjw
6-8 h: whhhhmhbhh
2-18 w: wswwwwwwwwwwwwwwwwww
11-12 v: vvvvvvvvvvvv
7-10 v: vrhvvcvvvvvv
4-9 r: rrrrmzntrsgrrkhhr
4-5 v: vvvtvc
3-10 t: ttnttwzgptr
2-3 c: gccrngp
2-3 b: bbbqx
1-7 s: zsdlsjs
5-7 j: jjjjjjj
2-5 g: ggnhg
2-6 w: fwwkwhjqbwqw
1-6 w: wdgnjwqwqv
9-11 b: bbtksbbdxbpjbbbbb
12-15 k: kkkkkkkkkkkkkkk
1-6 b: bbbrbbb
6-7 g: gggggvs
8-14 s: sssnwssrsssslcs
1-6 q: ngjprqqmdwgkjqvq
11-12 z: vzzkzzgzzzzzzz
3-4 n: nnnnnnnn
5-6 b: fbwbljf
11-18 h: hhhhhhhhhhshhhhhhlh
2-15 r: rrzgrzwrblrfwfrsd
6-11 n: zncnknknbgn
13-14 f: vwffftfffpffzfff
5-6 c: fcccbc
5-6 b: bxjbcpsbrgbgn
2-7 f: tffhrsf
7-12 g: ggggggtgggggg
4-5 t: ktphttltctt
4-6 w: fwsgwh
7-8 m: mfmmmmmmm
4-5 z: gzjzzfjzzz
8-9 l: lvtllllllll
3-5 m: rsmmd
10-11 r: rrrrrrrrrlz
11-13 j: hhqtkwznghbfsbwpvrj
4-5 z: zjzndzrmqzlptg
13-16 m: mmhmmmmmmmmmpmrmmm
5-7 p: pppphpp
2-3 s: zscgsj
14-15 h: hhhhhhhhhhhhhlhh
2-3 f: rffrtlxwfff
1-3 z: ptqlzzxskrjsnp
3-4 t: ftttst
8-9 t: thtttttpttm
11-13 g: qzhmgptdfwggqqc
6-9 f: nfffhfkffhf
2-3 w: qsxgww
11-12 d: ddddddddddddd
2-4 v: qsbvvv
8-13 z: zxczszkwxzgkz
2-4 t: cbtt
2-5 q: clndcnv
8-12 f: kfnhqbfnnpntfsvxcx
2-3 d: gddtd
1-9 z: tztzzzdnm
3-7 h: hhhhhhhhh
5-6 t: tttttvtttt
5-9 g: npzggdhvggpqgxwgvgsg
3-12 p: kwpptpplxrcgfxphpqbg
15-18 f: fffjffffffffffwffff
7-17 n: nnnnnnnnnnnnnnnnnnnn
5-10 d: dndddkddddx
1-7 v: cvhvsvhvv
5-7 f: fscrtxlz
1-14 b: bbbbbbbbbbbbbbb
1-11 n: nnlnxntnlxnnnktnnxb
13-18 p: pppzppppfqhppppzrp
7-8 h: hhhhzhhmqhh
16-17 r: rrrrwrrrrrrrrxrrrr
4-8 f: wfffgnff
15-16 j: jswvgtfhtffjgjjsxsdq
16-17 n: nnnsnnnnnnnnnnnnnnn
7-8 w: dxhcjvwwzwdddhhwwlj
1-13 h: ppshfshghphcq
5-15 b: cbxnbhbbjbhbbmbkvz
2-11 c: tdgcqlcccccthwmk
1-4 p: mpppp
1-4 x: xpzxxkzlrsgrbxpklcz
2-11 f: qffffgfffcgn
14-19 h: hhhhhhhhhhhhhsbkhhqh
9-12 h: hclhhhpsvhhfhh
2-6 s: lqcppxlsfv
6-9 v: vvvvvnvvv
9-10 q: xqqqqqqqqqq
3-8 m: qmmcjmvmckrmm
4-9 h: hhhdhfhhs
1-14 r: vpsprlstjkrmmrpzqsz
6-7 g: ggggrlqg
7-10 t: dtkttptttttt
3-4 n: vkkf
9-11 g: gflgwgggrgmmggg
3-19 l: blblmlrlllhbllwllll
12-13 x: gvxsxkrvdqbxx
11-12 b: bbbbdpdbbwls
2-13 n: nnnnnnnnsnnnrnnnnn
6-13 n: mfnnnnnkxnnnn
5-6 f: kxxfqnf
4-5 x: xxxxl
6-13 f: nsnxwrftkcgzffv
6-7 w: wwpwwxl
5-7 m: brvmmnw
1-4 r: rkbhnmdt
9-10 b: knbzbbfnfbbblrqbrbnj
2-5 v: vvvxvvxbw
3-5 q: qcsjq
7-15 c: gcdpccrcddccwccf
13-14 h: hhhhhhhhhhhhhh
10-11 l: rllllllllllpcs
10-11 w: wwwwwwwwwfw
2-7 f: fxtfffn
3-5 x: xxxkx
3-6 c: ccrrwbsj
1-4 g: gggggpgqgg
1-2 t: vztj
4-11 c: zccccccccqcc
5-11 p: hbpzppdlppp
3-5 h: frhhhzhprz
4-7 d: mzdnddd
15-16 t: gttttttttqttntttc
1-8 c: ccqcjbhpccczvfck
2-3 b: lztcb
4-5 d: ddddd
2-8 j: pwfjfdjjjjzkjcjrlwr
10-11 m: qmmmmmjnmpmmmmxn
10-17 f: ppbfdsjvpfhzccbwfr
4-8 c: cfnhqtcmcws
4-6 b: bbqxpfzqfvkpbhcbdfn
4-5 k: kkkkx
4-8 f: vpwfkfff
1-3 t: ttxtvtthttg
3-4 j: fjjj
18-19 v: vvvvvvvvvvvvvgvvvxp
11-14 q: qqqqqqqqqqqqqq
2-3 p: ppplxprwvjmf
7-14 q: qpqxzqqqqqqgqmgqqgq
3-7 x: dqlxtkx
6-7 f: sfczgfffzfhffjkkf
1-4 x: xxxxfwxxxpmxd
4-5 b: jbbbbb
10-11 p: vrpvpppppppdppp
18-19 l: llllllllllllllllllll
4-10 v: jjzvnsfbbvqcdfq
16-18 d: ddddddddddddddddddd
13-14 t: ftttdtkrdtktttlttkd
3-17 d: ddkddfldddddddddr
10-20 v: hfzhttvvnkxqvvhlvvvq
5-6 t: tbwtszgwpntthtvttrsj
5-6 s: ssshsstds
2-4 h: hhhb
6-7 b: bbsbfbb
2-12 n: znvqnslfhnwnqr
16-17 g: gggkggggggggggggl
4-12 j: vjqdjjjtsjdjv
14-16 j: jjjdjljjjjjjjjjjjp
4-14 d: dddddtqdddddddd
3-4 m: mpmm
7-8 c: ccccccdc
3-8 l: clllhnlllrzzgll
9-10 c: ccccccccbx
3-4 k: kkkj
5-12 r: rrrtjxhxzbrrf
4-6 w: wwwkwhww
7-9 m: fqrmrmmmmgmmmtmb
3-11 w: kwwnwwwghhgw
9-11 k: kkkkkkpkkkktvkkk
5-9 w: hwqjflkkwdqmwmcw
12-16 h: hmhmhhhkxhghhhhhhhhh
1-4 z: zjzlhjpft
1-6 d: dddqdd
1-9 s: ssssssssss
2-4 d: wddf
7-9 k: nhzkkmkxkhkphkdkkd
12-15 m: khmmmmmmhgmmmmmmm
3-9 n: jnknnnnnn
6-8 g: ntgggfggmlrgcggf
12-15 g: gggghggggmgmggdgg
7-13 x: rxxxxxrxxxxxnqxwsxx
3-5 n: bnnbnnvdwnls
3-5 v: mvbnzk
2-8 n: npntnnnjnxknb
6-11 t: ttftttlqtttt
5-7 g: ggggmgp
5-7 x: xxbjcxw
1-8 d: ddddldddddddwdddd
2-4 s: csswjbfnjnm
3-4 m: mmmr
16-17 z: zzsspzjzzzzzznzzz
3-4 r: rvzpfr
3-5 p: wpgxpb
4-5 r: qlrrr
7-9 q: vqzzqqrqtf
6-14 c: qcfwcfcchcvzzgcl
2-11 r: brhlrbvrlrrr
7-13 l: lllllllllrlllll
2-7 w: twfwwdwvzwbw
4-5 z: zzzbzz
3-4 q: qzzt
3-11 h: hhhhhhhhhhhh
5-12 c: cdclccgcccxfczrlcc
5-11 p: lmgprpppvphpj
2-5 r: rgkqrdsrxlmddmkktpnr
1-4 m: zmgfmqmmmxfm
15-16 f: ffbffffffffffwvnf
19-20 l: ltllllllllllllllllhl
2-6 j: spthjkftw
3-10 r: rrrrrrfrrmrrrr
1-8 n: nndfhqxn
6-7 j: zjjjjjj
12-14 k: qvgcvhmzkkrcck
6-8 k: kkhbkhkkkk
10-12 s: ssrssxssslsj
9-18 h: dsmxxwndhgjpztkqhh
5-6 m: mmmmlf
4-5 x: xxxlh
9-15 b: bbbcsgbdcbbrbrfbbbbb
6-15 j: nfbjrpjjqjjxsbjt
1-6 c: cncpkcfwcd
2-4 m: xmmhmjbcmdmm
4-11 s: lhdssvkldhsqssdjgs
14-19 t: ttttttdtttttlttttttt
2-4 x: gxxxbxk
2-6 k: kkpqkkkz
10-11 w: wwwqwwwwcgqwww
8-9 s: sssssssss
2-4 v: vvvwsrb
12-16 s: rtcsfwssdswthfpf
3-11 g: fgnggtlgbgd
2-5 c: ggccc
10-15 h: hhhhhhlhpghhshh
1-5 n: nlhqnj
7-10 f: fnfffffhffff
5-6 t: tfttkvt
1-4 c: mgcsrv
10-17 g: bggpmggggpgggggwqh
3-7 p: pprvppp
5-8 n: xnnqndnznf
6-11 b: bbbbrbfbrcdb
3-10 k: pcfknlqkgkpzkknk
10-11 m: mmmmmmmmmmc
3-4 v: fwvvh
2-7 l: zlcvxwrhlnnlkhr
1-7 q: vqqqjqsqqqqvrqqq
6-8 d: ddddddddd
1-3 z: zkzlrz
5-11 w: wbfzwdwpwdfztsrxzcw
7-9 s: qsssclssl
3-5 l: cvmlgk
9-12 s: qsrssnsvsxshqsdtd
8-12 m: wmxsnmmmfwzmf
1-12 x: xxxxxdxxjxxdxxxxxx
4-10 r: rwrlbkkmqrxr
5-13 g: ggggggggghgggx
11-15 w: wwwwtwnwwwvwwft
9-10 q: qqqqqqqqwfq
6-13 q: qmhfqqxkmcjwqmc
2-8 r: rrrrcmrrrrq
12-14 m: mmmwmmmmmgmhmt
2-6 t: ttvtctt
3-6 r: qcrrpsxjttqrrr
7-8 g: gggggggk
13-15 w: wwwwwwwwwwwwwww
8-12 p: qqphwpjmwppfpvwrp
7-10 c: cccccwtccc
8-11 w: kwtwrwwwwcqwww
9-19 z: zzzzzzzzzzzzzzzzzzz
4-5 q: qqqsq
6-9 c: cxfcccgcccpc
7-9 d: nddlddxdgddzd
3-12 q: qrghqxqjqqdwqqq
14-15 w: wwwwwwwwwwwwwqmwwww
14-15 c: frcwcctccrccccc
2-5 v: vqvvdvv
3-4 r: rsrr
16-18 d: dddddddddddddddtdxdd
4-7 b: bfbbbbsbj
14-15 b: bbgbbbbbbfbbmbbbmb
2-7 b: pbrsbbb
15-16 q: qqqsqqqqqqqbnqqqqq
6-14 k: rbvkkvqrxfklck
2-6 c: klccdm
2-3 b: bpbbs
4-13 p: cppjppppppppmvpppp
1-2 k: kqkpq
8-9 r: rrrrrhrkr
7-9 t: tptftttgwtmt
8-12 g: gfhkgqggznqghqg
15-18 n: pnnnnnnnnnnnnnnnnnn
4-5 p: qpqpq
1-9 h: xhqhhhnthzmhhhrh
4-6 x: bzkxxxhxxwdzpkks
4-5 c: ccccc
9-10 v: vvvvvvvvvv
8-12 d: cdcxdzgddtcfdrxt
7-10 b: bbbsbpbbgbbnj
4-7 z: zznzzzq
1-5 z: zjbzzjdppsbvgbg
2-4 w: lwwwtvjwdjwsbps
1-2 z: bzbvbz
6-14 n: dnnnnqnnnnnnnn
15-18 r: rfbrrwqshczrnbxrvhzr
9-11 f: ffffffffpfz
1-4 k: rskn
5-6 n: njnnnb
3-5 z: zzzzz
7-8 n: nntnnnnn
8-14 k: kkkkkkfkkkkkkkkk
11-16 r: rrqrprrrrrrrrrsvrr
2-10 c: dncjfccpcccccccc
8-20 q: gztqpsqqvthwpfjlqxrq
6-16 n: nnnnnpnnnnnnnnbnnnnn
4-7 h: ghhhwzhhhhhh
10-12 z: zzqzzzcfxzzzwzz
1-3 k: tlkwlkskklxvnk
12-13 n: nnnqqnnnnnnfpn
1-3 l: lnlb
6-9 b: bvfkbrzbwdmbvbql
11-13 d: ddddtdwtjdkdqz
9-15 j: jjcljjjjqjvnjjfjjj
13-14 l: tpllllllllqlll
18-20 d: dddddddddddzdddddtdb
9-15 x: nxxxmnxxxxxxsxxxjwg
6-7 j: rjwvjzjjx
5-6 j: jjjjjjj
8-13 t: ttfttttttttttt
6-16 f: ffrffxfffffffffmf
3-4 f: mhxk
4-9 w: lnwnwwvtvwjww
9-12 d: dwdddqddpdddd
11-16 q: qqqqqqqjzqqpqqlq
2-14 k: zkskkmkrwrdkskq
5-8 v: vqxmvxvvj
3-5 z: zpbwzhrzzwqxr
3-5 c: cccgc
6-9 v: vvvtbvvvvtj
5-6 f: fjwfgdfgtf
1-2 s: svvsnsk
11-13 f: qfpwfmrcfcwfr
3-6 j: jjjxdd
3-6 n: kntwpnn
8-13 v: qgmgcrxvdvkbs
11-16 q: cqsqpqlzqqdhqcqrbgk
1-8 t: pptttttt
11-13 g: ggggggggggkgqgg
4-5 g: gggsgg
10-11 p: rpppfppppmpppp
6-7 q: qqqqqqq
3-11 f: pgfftfflctfd
3-8 s: lswnfsjjdsh
11-13 k: kkkkkkkkkkmksk
3-5 p: pppjpppg
3-4 z: zznrz
8-9 d: ntgdwtdmh
2-3 g: gggl
19-20 q: qqqqqxqqqqqqqqqqqqwd
4-11 n: ljgdnkgftmsvntnn
16-19 t: tttttttttttttttttttt";
    }
}
