using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IsUsable
{
    public GameObject _operator { get; }
    public GameObject _subject {get; }
    void UseMoment();

}

