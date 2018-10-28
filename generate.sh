#!/bin/bash

gnuplot -p -e "file='plot_generation/creation.txt'" plot_generation/plot.gnu > plot_generation/creation.svg
gnuplot -p -e "file='plot_generation/deletion.txt'" plot_generation/plot.gnu > plot_generation/deletion.svg
gnuplot -p -e "file='plot_generation/insertion.txt'" plot_generation/plot.gnu > plot_generation/insertion.svg
