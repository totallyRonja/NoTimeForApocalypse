using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOptionHolder {
    IEnumerator ChooseOption(int index);
}
