from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By


options = webdriver.ChromeOptions()
options.page_load_strategy = "eager"
# how can i leave the browser instead of closing after request execution
options.add_argument("--disable-extensions")
options.add_experimental_option("detach", True)
driver = webdriver.Chrome(options = options)
driver.get("http://www.python.org")
assert "Python" in driver.title
elem = driver.find_element(By.NAME, "q")
elem.clear()
elem.send_keys("pycon")
elem.send_keys(Keys.RETURN)
assert "no results found." not in driver.page_source
driver.close()