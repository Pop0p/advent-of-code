FILE_PATH = "./input.txt"

File.open(FILE_PATH, "r") do |f|
  p1_sum = p2_sum = 0
  f.each_line do |line|
    p2_max_g = p2_max_b = p2_max_r = 1
    is_game_valid = true
    game_name, game_details = line.split(":")
    game_id = game_name.delete("^0-9").strip.to_i()
    sets = game_details.split(";")
    sets.each do |set|
      dictionary = { "blue" => 0, "green" => 0, "red" => 0}
      picks = set.split(",")
      picks.each do |pick|
        count, color_name = pick.split(" ")
        dictionary[color_name] = count.strip.to_i()
      end
      if dictionary["red"] > 12 || dictionary["green"] > 13 || dictionary["blue"] > 14
        is_game_valid = false
      end
      p2_max_r = [dictionary["red"], p2_max_r].max
      p2_max_b = [dictionary["blue"], p2_max_b].max
      p2_max_g = [dictionary["green"], p2_max_g].max
    end
    if is_game_valid
      p1_sum += game_id
    end
    p2_sum += p2_max_r * p2_max_b * p2_max_g
  end
  puts p1_sum
  puts p2_sum
 end

