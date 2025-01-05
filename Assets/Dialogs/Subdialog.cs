using UnityEngine;
using System.Collections.Generic;

namespace Artemito
{

[System.Serializable]
public class Subdialog
{
        public int id;
        public string name;
        public List<DialogOption> options;
        public int lastOptionID = 0;
}

}