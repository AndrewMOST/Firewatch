from __future__ import absolute_import, division, print_function, unicode_literals
from flask import Flask, request, redirect, url_for, render_template, send_from_directory
import numpy as np
import tensorflow as tf
from tensorflow.keras import datasets, layers, models
from tensorflow import keras
import os
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3' 
import random
import pathlib

AUTOTUNE = tf.data.experimental.AUTOTUNE

everythingcool = True

currentdir = pathlib.Path(__file__).parent.absolute()

# Попытка загрузки моделей и весов.
try:
    resnetpath = str(currentdir) + "/model/resnet.h5"
    multiweights = str(currentdir) + "/weights/accurate/cp"

    resnetmodel = keras.models.load_model(resnetpath)
    resnetmodel.load_weights(multiweights)

    binarypath = str(currentdir) + "/model/binary.h5"
    binaryweights = str(currentdir) + "/binaryweights/bestweights/cp"

    binarymodel = keras.models.load_model(binarypath)
    binarymodel.load_weights(binaryweights)
except:
    # Эту строку все равно никто не увидит, а у меня это вызывает панику.
    print('AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA')
    everythingcool = False;

# Классы предсказаний.
multi_labels = ["Cloud", "Dust", "Haze", "Land", "Seaside", "Wildfire"]
binary_labels = ["Safe", "Wildfire"]

splitter = '*'


app = Flask(__name__, static_url_path='')

@app.route('/')


@app.route('/index')
def index():
    if everythingcool:
        return "000"
    else:
        return "200"

# Тут везде парс строки, потом обаботка нейросетью с помощью встроенных функций.
# потом собрание строки ответа и отправка.

@app.route('/predictmanybinary', methods=['POST'])
def predictmanybinary():
    received = request.data.decode("utf-8")
    paths = received.split(splitter)
    results = []

    for path in paths:
        try:
            # Добавление предсказания бинарной модели.
            results.append(make_binary_prediction(path))
        except:
            results.append('Error')

    return splitter.join(results)


@app.route('/predictmanymulti', methods=['POST'])
def predictmanymulti():
    received = request.data.decode("utf-8")
    paths = received.split(splitter)
    results = []

    for path in paths:
        try:
            # Добавление предсказания многокласовой модели.
            results.append(make_multi_prediction(path))
        except:
            results.append('Error')

    return splitter.join(results)


# Загрузка весов для многоклассовой модели.
@app.route('/uploadweightsmulti', methods=['POST'])
def uploadweightsmulti():
    received = request.data.decode("utf-8")
    path = received[0:-6]
    
    resnetmodel.load_weights(path)
    return "000"

# Загрузка весов для бинарной модели.
@app.route('/uploadweightsbinary', methods=['POST'])
def uploadweightsbinary():
    received = request.data.decode("utf-8")
    path = received[0:-6]

    binarymodel.load_weights(path)
    return "000"



# Предобработка изображения: месштабирование и нормализация.
def preprocess_image(image):
  image = tf.image.decode_image(image, channels=3)
  image = tf.image.resize(image, [64, 64])
  image /= 255.0
  return image

# Загрузка и предобработка изображения.
def load_and_preprocess_image(path):
  image = tf.io.read_file(path)
  return preprocess_image(image)

# Предсказание от многоклассовой модели.
def make_multi_prediction(path):
  image = load_and_preprocess_image(path)
  image = np.expand_dims(image, axis=0)
  return multi_labels[np.argmax(resnetmodel.predict(image))]

# Предсказание от бинарной модели.
def make_binary_prediction(path):
  image = load_and_preprocess_image(path)
  image = np.expand_dims(image, axis=0)
  return binary_labels[np.argmax(binarymodel.predict(image))]


app.run(threaded=False)
