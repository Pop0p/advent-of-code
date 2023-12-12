FILE_PATH = "./input.txt"

po_stacks, pt_stacks = {}
File.open(FILE_PATH, "r") do |f|
 # Retrieve the po_stacks
 f.each_line do |line|
    column = 1
    if line.count("0-9") > 0
      break
    end
      
    line.chars.each_slice(4) do |crate|
      col = po_stacks.fetch(column, Array.new)
      if !crate.join().strip().empty?
        col.unshift(crate.join().strip())
      end
      po_stacks[column] = col
      column += 1
    end
  end

 pt_stacks = Marshal.load(Marshal.dump(po_stacks)) 
 # Parse instructions
 f.each_line do |line|
   if line.strip().empty?
     next
   end
   instructions = line.delete('^0-9 ').split().map(&:to_i)
   (po_stacks[instructions[2]] << po_stacks[instructions[1]].pop(instructions[0]).reverse).flatten!
   (pt_stacks[instructions[2]] << pt_stacks[instructions[1]].pop(instructions[0])).flatten!
 end
end
po_stacks.each { |k, v| print v.last.delete('[]') }
puts "\n"
pt_stacks.each { |k, v| print v.last.delete('[]') }
