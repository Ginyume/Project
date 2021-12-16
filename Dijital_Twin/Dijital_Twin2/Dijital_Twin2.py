# -*- coding: utf-8 -*-
import numpy as np
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
from sklearn.tree import DecisionTreeRegressor
from sklearn.svm import SVR
from sklearn.preprocessing import StandardScaler

veriler =pd.read_csv('veriler2.csv', sep=';', engine='python')
Usage=veriler.iloc[:,0]
Temp=veriler.iloc[:,1]
Frequ=veriler.iloc[:,2]
Power_Draw=veriler.iloc[:,3]
s=pd.concat([Usage,Frequ], axis=1)
s2=pd.concat([s,Power_Draw], axis=1)
x_train, x_test,y_train,y_test = train_test_split(s2,Temp,test_size=0.33, random_state=0)

#Liner Regrosyon İle Deneme
LR=LinearRegression()
LR.fit(x_train,y_train)
print(LR.predict(x_test))

#SVR ile Deneme
SC=StandardScaler()
s2_SC=SC.fit_transform(s2)
Temp_SC = np.ravel(SC.fit_transform(Temp.values.reshape(-1,1)))
SVR = SVR(kernel='rbf')
SVR.fit(s2_SC,Temp_SC)
print(SVR.predict(x_test))

#Decision Tree ile Deneme
DT=DecisionTreeRegressor(random_state=0)
DT.fit(x_train,y_train)
print(DT.predict(x_test))



