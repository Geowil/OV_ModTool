using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OV_ModTool_V1._0
{
    class dataObj
    {
        //Functions

        //The following fuctions are broken down into data type, meaning there will be a set function for every table in the database.
        //Params will correspond to the fields in that table and set the appropriate members for the instance of this class
        void setAPlate(string nm, string desc, string cls, int id, int tlvl, int rdly, float pwrreq, float cpureq, float ramreq, float drb, float pwrcons, float reprate, float sg2,
                       int xcost, int rcost, int dicost, int dcost, int lcost, int ucost, int pcost, int nwcost) {
            dAPlt.name = nm;
            dAPlt.desc = desc;
            dAPlt.cls = cls;
            dAPlt.id = id;
            dAPlt.tlvl = tlvl;
            dAPlt.rdly = rdly;
            dAPlt.pwrreq = pwrreq;
            dAPlt.cpureq = cpureq;
            dAPlt.ramreq = ramreq;
            dAPlt.drb = drb;
            dAPlt.pwrcons = pwrcons;
            dAPlt.reprate = reprate;
            dAPlt.sg2 = sg2;
            dAPlt.xcost = xcost;
            dAPlt.rcost = rcost;
            dAPlt.dcost = dcost;
            dAPlt.dicost = dicost;
            dAPlt.lcost = lcost;
            dAPlt.ucost = ucost;
            dAPlt.pcost = pcost;
            dAPlt.nwcost = nwcost;
        }

        aplating getAPlate() => dAPlt;
        cap getCap() => dCap;
        clone getClone() => dClone;
        cpu getCpu() => dCPU;
        dispo getDisp() => dDisp;
        eng getEng() => dEng;
        equip getEquip() => dEquip;
        hull getHull() => dHull;
        msg getMsg() => dMsg;
        miss getMission() => dMiss;
        mreward getMReward() => dMReward;
        name getName() => dName;
        npcship getNPCShp() => dNpcShp;
        odp getODP() => dODP;
        pshd getPShd() => dPShd;
        psys getPSys() => dPSys;
        race getRace() => dRace;
        ram getRAM() => dRam;
        rank getRank() => dRank;
        rproc getReproc() => dRpc;
        resc getResc() => dResc;
        setting getSetting() => dSetting;
        shpcls getShpCls() => dShpCls;
        ship getShp() => dShp;
        skill getSk() => dSk;
        skltraining getSkTrning() => dSkTrning;
        smtx getSMtx() => dSMtx;
        sysmane getSysNm() => dSysNm;
        weapon getWeap() => dWeap;
        wfgen getWFGen() => dWFGen;
        wtsys getWTSys() => dWTSys;

        //Members
        aplating dAPlt;
        cap dCap;
        clone dClone;
        cpu dCPU;
        dispo dDisp;
        eng dEng;
        equip dEquip;
        hull dHull;
        msg dMsg;
        miss dMiss;
        mreward dMReward;
        name dName;
        npcship dNpcShp;
        odp dODP;
        pshd dPShd;
        psys dPSys;
        race dRace;
        ram dRam;
        rank dRank;
        rproc dRpc;
        resc dResc;
        setting dSetting;
        shpcls dShpCls;
        ship dShp;
        skill dSk;
        skltraining dSkTrning;
        smtx dSMtx;
        sysmane dSysNm;
        weapon dWeap;
        wfgen dWFGen;
        wtsys dWTSys;


    }
}
