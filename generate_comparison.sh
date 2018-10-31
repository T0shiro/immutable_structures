#!/bin/bash

for struct in RedBlack_vs_AVL heap_vs_AVL
do
    struct1=$(echo $struct | cut -d _ -f1)
    struct2=${struct##*_}
    for op in creation insertion deletion search
    do
        gnuplot -p -e "file='plot_generation/${struct}_$op.txt'" -e "struct1='$struct1'" -e "struct2='$struct2'" plot_generation/plot_comparison.gnu > plot_generation/${struct}_$op.svg
    done
done