# -*- coding: utf-8 -*-
"""
Created on ~

@author: VinLes
"""


# reformat q-table (gamesxrewards) from numpy array to c# dictionary (coordinatesx1)

import joblib
import numpy as np


def read_lab(file='main.txt'):
    lab = []
    with open(file, 'r') as labfile:
        lab = labfile.readlines()
    lab = [[int(y) for y in list(x.replace('P', '0').replace('E', '0').strip())]
           for x in lab]
    return lab


lab = read_lab()

##############Name of your object tp format!! goes here
Q = joblib.load("53Q_tabelv.qt")
Q_BACK = joblib.load("Q_state_dictionary1.dic")


free = []
for row in range(len(lab)):
    for col in range(len(lab[row])):
        if lab[row][col] == 0:
            free.append([row, col])


free_walls_dict = {}
for rc in free:
    walls = []  # up
    if (lab[rc[0]-1][rc[1]]) == 0:
        walls.append(1)
    else:
        walls.append(0)

# down
    if (lab[rc[0]+1][rc[1]]) == 0:
        walls.append(1)
    else:
        walls.append(0)

# left
    if (lab[rc[0]][rc[1]-1]) == 0:
        walls.append(1)
    else:
        walls.append(0)

# right
    if (lab[rc[0]][rc[1]+1]) == 0:
        walls.append(1)
    else:
        walls.append(0)

    free_walls_dict[str(rc[0]) + "_" + str(rc[1])] = walls


def possible(x, y):
    possible = free_walls_dict[str(y) + "_" + str(x)]
    moves_i = []
    for i in range(4):
        if possible[i] == 1:
            moves_i.append(i)
    return moves_i


all_possible_coordinates = []  # in tuples
for y in range(np.array(lab).shape[0]):
    for x in range(np.array(lab).shape[1]):
        all_possible_coordinates.append((y, x))


CNT = 0
d = {0: 0, 1: 1, 2: 2, 3: 3}


with open('dic_ind.txt', 'a') as f:
    for k, yx_agent in enumerate(all_possible_coordinates):
        line = '''qI.Add("{}_{}", {});'''.format(yx_agent[0], yx_agent[1], k)
        f.write(line)
        f.write('\n')


D = {}
with open('dic.txt', 'a') as f:
    for yx_agent in all_possible_coordinates:
        movess = []
        for yx_fruit in all_possible_coordinates:
            try:
                key = str([yx_agent[0], yx_agent[1], yx_fruit[0], yx_fruit[1]])
                moves = Q[Q_BACK[str(key)]]
                moves_i = possible(yx_agent[1], yx_agent[0])
                if np.argmax(moves) in moves_i:
                    # then legal move
                    CNT += 1

                    #line = '''q.Add("{}_{}_{}_{}", "{}");'''.format(yx_agent[0],yx_agent[1],yx_fruit[0],yx_fruit[1],d[np.argmax(moves)])
                    # f.write(line)
                    # f.write('\n')
                    key = "{}_{}_{}_{}".format(
                        yx_agent[0], yx_agent[1], yx_fruit[0], yx_fruit[1])
                    movess.append(np.argmax(moves))
                    D[key] = d[np.argmax(moves)]

                else:

                    # if wall
                    #line = '''qTableProps.Add("{}_{}__{}_{}", "{});'''.format(yx_agent[0],yx_agent[1],yx_fruit[0],yx_fruit[1],'None')
                    # f.write(line)
                    key = "{}_{}_{}_{}".format(
                        yx_agent[0], yx_agent[1], yx_fruit[0], yx_fruit[1])
                    #D[key] = 4
                    movess.append(4)
            except:

                # if was not in free moves of q table
                #line = '''qTableProps.Add("{}_{}__{}_{}", "{});'''.format(yx_agent[0],yx_agent[1],yx_fruit[0],yx_fruit[1],'wall')
                # f.write(line)
                key = "{}_{}_{}_{}".format(
                    yx_agent[0], yx_agent[1], yx_fruit[0], yx_fruit[1])
                #D[key] = 5
                movess.append(5)
        line = '''q.Add("{}_{}", "{}");'''.format(
            yx_agent[0], yx_agent[1], ''.join([str(i) for i in movess]))
        f.write(line)
        f.write('\n')


print(CNT, np.array(lab).shape[0]*np.array(lab).shape[1]*2)

#import json

# with open('qt.json', 'w') as fp:
#    json.dump(D, fp)
