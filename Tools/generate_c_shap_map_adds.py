# -*- coding: utf-8 -*-
"""
Created on ~

@author: VinLes
"""

import numpy as np

#read map in python -> obtain dictionary adding for c#


def read_lab(file='map.txt'):
    lab = []
    with open(file, 'r') as labfile:
        lab = labfile.readlines()
    lab = [[int(y) for y in list(x.replace('P', '0').replace('E', '0').strip())]
           for x in lab]
    return lab


lab = read_lab()

print(np.array(lab).shape)
for row in range(len(lab)):
    for col in range(len(lab[row])):
        if lab[row][col] == 1:
            tp = 'wall'
        else:
            tp = 'tile'
        print('''gridProps.Add("{}__{}", "{}");'''.format(col, row, tp))
