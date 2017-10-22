using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OV_ModTool_V1._0
{
    public struct aplating
    {
        public string name, desc, cls; //1-3
        public int id, tlvl, rdly; //0,4,11
        public float pwrreq, cpureq, ramreq, drb, pwrcons, reprate, sg2; //5-10,12
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //13-20
    };

    public struct cap
    {
        public string name, desc; //1,2
        public int id, tlvl; //0,3
        public float cpureq, ramreq, capt, rchgrate, rchgamt, sg2; //4-9
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //10-17
    };

    public struct clone
    {
        public string name, desc, cls; //1-3
        public int id, tlvl; //0,4
        public float sg2; //5
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //6-13
    };

    //clone effect goes here

    public struct cpu
    {
        public string name, desc, cls; //1-3
        public int id, tlvl; //0,4
        public float pwrreq, capt, sg2; //5-7
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //8-15
    };

    public struct dispo
    {
        public string disp; //1
        public int id, relval; //0,2
    };

    //effects
    //effect effect modifiers xref
    //effect modifiers

    public struct eng
    {
        public string name, desc, cls; //1-3
        public int id, tlvl; //0,4
        public float pwrreq, cpureq, ramreq, maxspd, acclrate, navadjrate, sg2; //5-11
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //12-19
    };

    public struct equip
    {
        public string name, desc, type, cls, subcls; //1-5
        public int id, tlvl, targtime; //0,6,10
        public float mindmg, maxdmg, maxdist, pwrreq, cpureq, ramreq, rchgrate, pwrcons, initpwrcon, dlytime, sg2; //7-9,11-18
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //19-26
    };

    //equipment effect xref

    public struct hull
    {
        public string name, desc, cls; //1-3
        public int id, tlvl, repdly; //0,4,11
        public float pwrreq, cpureq, ramreq, reprate, pwrcons, repamt, drb, sg2; //5-10,12,13
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //14-21
    };

    public struct msg
    {
        public string mg; //1
        public int id; //0
    };

    public struct miss
    {
        public string name, desc, diff, sec, sys, targnm, targshpcls, type; //1-2,5-10
        public int id, reqcel, tlim; //0,3,4
    };

    public struct mreward
    {
        public string rwdtyp; //1
        public int id, rwdamt; //0,2
    };

    //mission mission rewards xref

    public struct name
    {
        public string nm, nmtyp, applraces; //1-3
    };

    public struct npcship
    {
        public string name, shpcls; //1-2
        public int id, minlvrng, maxlvrng, basecxp; //0,3-5
    };

    public struct odp
    {
        public string name, desc, cls, subcls; //1-4
        public int id, tlvl, cpu, ram, psys, cap, smtx, aplt, hull, wtsys, tslots, lslots, mslots, hslots, cxp; //0,5-17,28
        public float sg2, cargo; //18-19
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //20-27
    };

    public struct pshd
    {
        public string name, desc; //1-2
        public int id, tlvl, psys, cpu, ram, smtx, tslots, lslots, mslots; //0,3-10
        public float sg2; //11
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //12-19
    };

    public struct psys
    {
        public string name, desc, typ; //1-3
        public int id, tlvl; //0,4
        public float cpureq, ramreq, capt, sg2; //5-8
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //9-16
    };

    public struct race
    {
        public string name, desc, emp, cap; //1-4
    };

    public struct ram
    {
        public string name, desc; //1-2
        public int id, tlvl; //0,3
        public float pwrreq, cpureq, capt, sg2; //4-7
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //8-15
    };

    public struct rank
    {
        public string name; //1
    };

    public struct rproc
    {
        public int id, oid, rid, rescost; //0-2,5
        public float repunits, resprod, resprodloss; //3-4,6
    };

    //Requirement xref

    public struct resc
    {
        public string name, desc, typ; //1-2,4,
        public int id, stg, tier, xcost; //0,5,6,8
        public float sg2, bsize; //3,7
    };

    public struct setting
    {
        public string sKey; //1
        public string sVal; //2
    };

    public struct shpcls
    {
        public string name; //1
        public int id; //0
    };

    public struct ship
    {
        public string name, desc, cls, subcls; //1-4
        public int id, tlvl, cpu, ram, psys, cap, eng, wfgen, smtx, aplt, hull, wtsys, tslots, lslots, mslots, hslots; //0,5-19
        public float sg2, crg; //20-21
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //22-28
    };

    public struct skill
    {
        public string name, desc, typ, sgrp; //1-4
        public int id, ttm, xcost, cxpcost; //0,5,8-9
        public float sg2; //6
    };

    public struct skltraining
    {
        public int id, skid; //0-1
        public float rk1TMod, rk2TMod, rk3TMod, rk4TMod, rk5TMod, rk6TMod, rk7TMod, rk8TMod, rk9TMod, rk10TMod; //2-11
    };

    public struct smtx
    {
        public string name, desc; //1-2
        public int id, tlvl; //0,3
        public float pwrreq, cpureq, ramreq, rchgrate, pwrcons, rchgamt, rchgdly, capt, sg2; //4-12
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //13-20
    };

    public struct sysmane
    {
        public string name; //1
        public int id; //0
    };

    public struct weapon
    {
        public string name, desc, cls, subcls; //1-4
        public int id, tlvl, ammocap, ammocons; //0,5,9-10
        public float pwrreq, cpureq, ramreq, mindmg, maxdmg, dmgintlow, dmginthi, mineffrng, maxeffrng, minopteffrng, maxopteffrng, atkspd, pwrcons, initpwrcon, mcdur, mcyld, mass, sg2; //6-8,11-25
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //26-33
    };

    //weapon ammo xref

    public struct wfgen
    {
        public string name, desc; //1-2
        public int id, tlvl; //0,3
        public float pwrreq, cpureq, ramreq, pwrcons, wrpstblrate, maxspd, sg2; //4-10
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //11-18
    };

    public struct wtsys
    {
        public string name, desc; //1-2
        public int id, tlvl, maxtrg, trgrate; //0,3-5
        public float pwrreq, cpureq, ramreq, maxscnrng, sg2; //6-10
        public int xcost, rcost, dcost, dicost, lcost, ucost, pcost, nwcost; //11-18
    };
}