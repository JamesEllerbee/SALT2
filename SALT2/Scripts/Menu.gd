extends Control

func _ready():
	$VBoxContainer/StartButton.grab_focus()

func _on_StartButton_pressed():
	get_tree().change_scene("res://Scenes/dev_test1.tscn")

#TODO: Add funcitonality for options menu

func _on_QuitButton_pressed():
	get_tree().quit()
