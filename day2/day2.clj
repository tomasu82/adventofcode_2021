(require '[clojure.string :as str])

(def input (map #(str/split %1 #" ") (str/split (slurp "input1.txt") #"\n")))

(defn compu [coord inst]
       (let [[dir m] inst
             [horizontal depth aim] coord
             mag (Integer/parseInt m)]
              (cond (= dir "forward") [(+ horizontal mag) (+ depth (* aim mag)) aim]
                    (= dir "up") [horizontal depth (- aim mag)]
                    :else [horizontal depth (+ aim mag)]
              )))
              
(reduce compu [0 0 0] input)